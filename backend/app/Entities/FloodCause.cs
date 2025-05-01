using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Early_warning
{
    public class FloodCause
    {
        public int Id { get; set; }
        public string Cause { get; set; }
        public ICollection<FloodEvent> FloodEvents { get; set; }
    }

}
