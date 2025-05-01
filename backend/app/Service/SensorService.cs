using Early_warning.Contexts;
using Early_warning.Entities;
using Early_warning.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Early_warning.Service
{
    public class SensorService
    {
        private readonly SensorDataContext _context;
      //  private readonly OnnxModelService _onnxService;
        private readonly IHubContext<SensorHub> _hubContext;

        public SensorService(SensorDataContext context,  IHubContext<SensorHub> hubContext)
        {
            _context = context;
           
            _hubContext = hubContext;
        }

        public async Task<List<string>> ProcessSensorData(SensorData data)
        {
            var alerts = new List<string>();

            try
            {
                // تحديد الوقت الحالي إذا غير محدد
                if (data.Timestamp == default)
                    data.Timestamp = DateTime.UtcNow;

                // التحقق من التكرار
                var exists = await _context.SensorsData.AnyAsync(s =>
                    s.Temperature == data.Temperature &&
                    s.Humidity == data.Humidity &&
                    s.SmokeLevel == data.SmokeLevel &&
                    s.CarbonMonoxideLevel == data.CarbonMonoxideLevel &&
                    s.FlameDetected == data.FlameDetected);

                if (!exists)
                {
                    if (data.FlameDetected)
                    {
                       
                            var anomalousData = new AnomalousData
                            {
                                Temperature = (int)data.Temperature,
                                Humidity = (int)data.Humidity,
                                SmokeLevel = (int)data.SmokeLevel,
                                CarbonMonoxideLevel = (int)data.CarbonMonoxideLevel,

                                FlameDetected = data.FlameDetected,
                                Timestamp = data.Timestamp
                               
                            };
                            await _context.AnomalousSensorData.AddAsync(anomalousData);
                        
                        
                            await _context.SensorsData.AddAsync(data);
                        

                        // ✅ إرسال البيانات للواجهة عندما يوجد لهب
                        await _hubContext.Clients.All.SendAsync("ReceiveSensorData", data);
                    }
                    else
                    {
                        // تخزين كقيمة شاذة مباشرة عند عدم وجود لهب
                        var anomalousData = new AnomalousData
                        {
                            Temperature = (int)data.Temperature,
                            Humidity = (int)data.Humidity,
                            SmokeLevel = (int)data.SmokeLevel,
                            CarbonMonoxideLevel = (int)data.CarbonMonoxideLevel,

                            FlameDetected = data.FlameDetected,
                            Timestamp = data.Timestamp
                            
                        };
                        await _context.AnomalousSensorData.AddAsync(anomalousData);

                        // ❌ عدم إرسال البيانات
                        // ✅ إرسال رسالة خطأ إلى الواجهة
                        await _hubContext.Clients.All.SendAsync("ReceiveAlert", "Flame is false. No action taken. Possible device error.");
                    }


                    await _context.SaveChangesAsync();
                }

             

                // التحقق من التنبيهات
                alerts = ValidateSensorData(data);
                foreach (var alert in alerts.Where(a => a != "SAFE"))
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveAlert", alert);
                }

                return alerts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing sensor data: {ex.Message}");
                throw;
            }
        }
        public List<string> ValidateSensorData(SensorData data)
        {
            var alerts = new List<string>();

            // فحص درجة الحرارة مع عرض القيمة
            if (data.Temperature < -20 || data.Temperature > 60)
                alerts.Add($"CRITICAL_TEMPERATURE_OUT_OF_RANGE (القيمة: {data.Temperature}°C)");
            else if (data.Temperature < 0 || data.Temperature > 50)
                alerts.Add($"WARNING_TEMPERATURE_OUT_OF_RANGE (القيمة: {data.Temperature}°C)");

            // فحص الرطوبة مع عرض القيمة
            if (data.Humidity < 10 || data.Humidity > 95)
                alerts.Add($"CRITICAL_HUMIDITY_OUT_OF_RANGE (القيمة: {data.Humidity}%)");
            else if (data.Humidity < 20 || data.Humidity > 85)
                alerts.Add($"WARNING_HUMIDITY_OUT_OF_RANGE (القيمة: {data.Humidity}%)");

            // فحص الدخان مع عرض القيمة
            if (data.SmokeLevel > 1000)
                alerts.Add($"CRITICAL_SMOKE_LEVEL_HIGH (القيمة: {data.SmokeLevel}PPM)");
            else if (data.SmokeLevel > 300)
                alerts.Add($"WARNING_SMOKE_LEVEL_HIGH (القيمة: {data.SmokeLevel}PPM)");

            // فحص أول أكسيد الكربون مع عرض القيمة
            if (data.CarbonMonoxideLevel > 50)
                alerts.Add($"CRITICAL_CO_LEVEL_HIGH (القيمة: {data.CarbonMonoxideLevel}PPM)");
            else if (data.CarbonMonoxideLevel > 20)
                alerts.Add($"WARNING_CO_LEVEL_HIGH (القيمة: {data.CarbonMonoxideLevel}PPM)");

            // كشف اللهب مع عرض الحالة
            if (data.FlameDetected)
                alerts.Add("CRITICAL_FLAME_DETECTED (تم الكشف عن لهب)");

            // إذا لم يكن هناك أي تنبيهات
            if (alerts.Count == 0)
                alerts.Add("SAFE (جميع القيم ضمن النطاق الآمن)");

            return alerts;
        }
        public async Task<IEnumerable<AnomalousData>> GetAnomalousSensorData()
        {
            return await _context.AnomalousSensorData
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<List<SensorData>> GetHistoricalData()
        {
            return await _context.SensorsData
                .OrderByDescending(s => s.Timestamp)
                .Take(100) // الحد إلى 100 قراءة حديثة
                .ToListAsync();
        }
    }
}
