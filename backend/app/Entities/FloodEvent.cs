using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Early_warning;

namespace Early_warning
{

    public class FloodEvent
    {

        public int Id { get; set; }

        public int LocationId { get; set; }  // Foreign Key for Location
        public int Year { get; set; }
        public int SeverityId { get; set; }  // Foreign Key for Severity
        public int CauseId { get; set; }  // Foreign Key for Cause
        public string Impact { get; set; }



        // Navigation properties with explicit foreign key mappings
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; }

        [ForeignKey(nameof(SeverityId))]
        public SeverityLevel Severity { get; set; }

        [ForeignKey(nameof(CauseId))]
        public FloodCause Cause { get; set; }
    }
}
