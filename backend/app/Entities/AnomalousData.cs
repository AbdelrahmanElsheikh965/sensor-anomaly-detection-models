namespace Early_warning.Entities
{
    public class AnomalousData
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float SmokeLevel { get; set; }
        public float CarbonMonoxideLevel { get; set; }
        public bool FlameDetected { get; set; }
        public DateTime Timestamp { get; set; }
       
    }
}
