using System;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class Analysis
    {
        [Key]
        public Guid AnalysisId { get; set; } // GUID as primary key
        public string UserId { get; set; } // Foreign key
        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; internal set; }

        public Analysis()
        {
            AnalysisId = Guid.NewGuid(); // Auto-generate GUID for new instances
        }
    }
}
