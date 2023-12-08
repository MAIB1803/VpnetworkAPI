using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class ThresholdSettings
    {


        [ForeignKey("User")]
        public string UserId { get; set; }

        // ForeignKey is not necessary here
        [Key]
        public string ProgramName { get; set; }
        public string ThresholdSetting { get; set; }
    }
}
