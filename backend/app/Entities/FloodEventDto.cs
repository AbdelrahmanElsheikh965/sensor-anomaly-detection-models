namespace Early_warning.Entities
{
  
        public class FloodEventDto
        {
        public int Id { get; set; }

        public string City { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public string Impact { get; set; }
            public int Year { get; set; }
            public string Cause { get; set; }
            public string Level { get; set; }
        }

    }

