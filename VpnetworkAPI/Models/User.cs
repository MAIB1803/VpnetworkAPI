using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VpnetworkAPI.Models
{
    public class User
    {
        [Key]
        public String UserId { get; set; }
        public ICollection<ProgramData> ProgramData { get; set; }

        public ICollection<LocalProgramData> LocalProgramData { get; set; }
        public ICollection<ThresholdSettings> ThresholdSettings { get; set; }
        public ICollection<Analysis> Analyses { get; set; } // One-to-many relationship

        public User()
        {
            ProgramData = new HashSet<ProgramData>();
            LocalProgramData = new HashSet<LocalProgramData>();
            ThresholdSettings = new HashSet<ThresholdSettings>();
            Analyses = new HashSet<Analysis>();
        }
    }
}
