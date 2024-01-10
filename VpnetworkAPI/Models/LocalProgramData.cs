using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class LocalProgramData
    {
        [Key]
        public Guid LocalProgramDataId { get; set; }
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public double ProgramLocalNetworkThreshold { get; set; }
        public double ProgramLocalMemoryThreshold { get; set; }
        public bool? IsHarmful { get; set; }

        public User User { get; set; }
    }
    }
    

