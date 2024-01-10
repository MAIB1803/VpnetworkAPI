using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VpnetworkAPI.Models
{
    public class Analysis
    {
        [Key]
        public Guid AnalysisId { get; set; }
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public DateTime DateTime { get; set; }

        public User User { get; set; }
    }
    }
