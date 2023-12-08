using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VpnetworkAPI.Models
{

    public class User
    {
        [Key]
        public string UserId { get; set; }

        public List<ProgramData> Programs { get; set; }

        // Include LocalProgramData and ThresholdSettings directly
        public Settings? UserSettings { get; set; }

        public class Settings
        {
            [Key, ForeignKey("User")]
            public string UserId { get; set; }


            public List<LocalProgramData> LocalProgramSettings { get; set; } = new List<LocalProgramData>();

            // Include ThresholdSettings directly
            public List<ThresholdSettings> ThresholdtypeSettings { get; set; } = new List<ThresholdSettings>();
        }
    }

}

