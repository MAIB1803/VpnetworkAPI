using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class GlobalProgramData
    {
        [Key]
        public string ProgramName { get; set; }
        public double ProgramGlobalNetworkThreshold { get; set; }
        public double ProgramGLobalMemoryThreshold { get; set; }
        public bool IsHarmful { get; set; }
    }
}
