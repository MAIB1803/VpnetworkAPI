using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class ProgramData
    {

        [Key]
        public string ProgramName { get; set; }
        public int PID { get; set; }
        public long MemoryUsage { get; set; }

        public double NetworkUsage { get; set; }
        public int ProgramBadCount { get; set; }

    }
}
