using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class ThresholdSettings
    {


        [ForeignKey("User")]
        public string UserId { get; set; }
        [Key]
        public Guid ThresholdSettingsDataId { get; set; }
        public string ProgramName { get; set; }
        public string ThresholdSetting { get; set; }
        public User User { get; set; }
        public ThresholdSettings()
        {
            ThresholdSettingsDataId = Guid.NewGuid(); // Auto-generate GUID for new instances
        }
    }
}
