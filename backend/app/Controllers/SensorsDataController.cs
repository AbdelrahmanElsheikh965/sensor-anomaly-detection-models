using Microsoft.AspNetCore.Mvc;
using Early_warning.Service;
using System.Threading.Tasks;

namespace Early_warning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly SensorService _sensorDataService;

        public SensorDataController(SensorService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessSensorData([FromBody] SensorData data)
        {
            try
            {
                var alerts = await _sensorDataService.ProcessSensorData(data);

                var response = new
                {
                    ReceivedData = data,
                    Alerts = alerts
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("anomalous")]
        public async Task<IActionResult> GetAnomalousSensorData()
        {
            try
            {
                var data = await _sensorDataService.GetAnomalousSensorData();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}