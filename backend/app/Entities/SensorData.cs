using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Early_warning
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; }
        public float Temperature { get; set; }       // in °C
        public float Humidity { get; set; }             // in %
        public float SmokeLevel { get; set; }           // in PPM
        public float CarbonMonoxideLevel { get; set; }  // in PPM
        public bool FlameDetected { get; set; }       // boolean (0 or 1)
        public DateTime Timestamp { get; set; }       // DateTime of the reading
    }
}
