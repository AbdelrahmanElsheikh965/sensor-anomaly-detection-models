using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Early_warning
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        //Longitude
        public ICollection<FloodEvent> FloodEvents { get; set; }
    }
}
