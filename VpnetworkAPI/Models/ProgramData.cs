﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VpnetworkAPI.Models
{
    public class ProgramData
    {
        [Key]
        public Guid ProgramDataId { get; set; }
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public int ProgramBadCount { get; set; }

        public User User { get; set; }

    }
}
