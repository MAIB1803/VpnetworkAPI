using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class LocalProgramData
    {
        [Key]
        public Guid LocalProgramDataId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public double ProgramLocalNetworkThreshold { get; set; }
        public double ProgramLocalMemoryThreshold { get; set; }
        public bool IsHarmful { get; set; } = false;
        public User User { get; set; }
        public LocalProgramData()
        {
            LocalProgramDataId = Guid.NewGuid(); // Auto-generate GUID for new instances
        }
    }
    
}
