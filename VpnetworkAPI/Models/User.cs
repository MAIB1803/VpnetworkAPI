 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VpnetworkAPI.Dto;

namespace VpnetworkAPI.Models
{
    public class User
    {
        internal List<ProgramDataDto> programs;

        [Key]
        public string UserId { get; set; }
        public string User_Name { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string UserImage { get; set; }
        

        public ICollection<Analysis> Analysis { get; set; }
        public ICollection<LocalProgramData> LocalProgramData { get; set; }
        public ICollection<ProgramData> ProgramData { get; set; }
        public ICollection<ThresholdSettings> ThresholdSettings { get; set; }
    }
}
