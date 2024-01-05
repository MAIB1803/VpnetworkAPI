using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    
    public class Analysis
    {
        public string UserId { get; set; }
        [Key]
        public DateTime DateTime { get; set; }
        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
    }
}
