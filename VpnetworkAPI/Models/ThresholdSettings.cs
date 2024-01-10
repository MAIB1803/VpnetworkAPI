using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class ThresholdSettings
    {
        [Key]
        public Guid ThresholdSettingDataId { get; set; }
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public string ThresholdSetting { get; set; }

        public User User { get; set; }
    }
}
