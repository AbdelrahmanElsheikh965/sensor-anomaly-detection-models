/* using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Early_warning.Service
{
public class OnnxModelService
   {
       private readonly InferenceSession _session;
       private readonly ILogger<OnnxModelService> _logger;

       public OnnxModelService(string modelPath, ILogger<OnnxModelService> logger)
       {
           _logger = logger;
           _session = new InferenceSession(modelPath);
       }

       public bool PredictAnomaly(SensorData data)
       {
           try
           {
               // تحويل البيانات إلى مصفوفة
               var inputData = new float[]
               {
                   (float)data.Temperature,
                   data.Humidity,
                   data.SmokeLevel,
                   data.CarbonMonoxideLevel,
                   data.FlameDetected ? 1f : 0f
               };

               // إنشاء التنسور
               var tensor = new DenseTensor<float>(inputData, new[] { 1, 5 });

               // تشغيل النموذج
               var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("input", tensor) };
               var results = _session.Run(inputs);

               // الحصول على النتيجة
               var score = results.First().AsEnumerable<float>().First();

               return score > 0.5f;
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Error in prediction: {ex.Message}");
               return false;
           }
       }
   }
}*/