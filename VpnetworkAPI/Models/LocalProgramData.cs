using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class LocalProgramData
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Key]
        public string ProgramName { get; set; }
        public double ProgramLocalNetworkThreshold { get; set; }
        public double ProgramLocalMemoryThreshold { get; set; }
    }
}
