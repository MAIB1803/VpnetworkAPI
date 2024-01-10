using VpnetworkAPI.Models;

namespace VpnetworkAPI.Dto
{
    public class ProgramDataDto
    {
        public Guid ProgramDataId { get; set; }
        public string UserId { get; set; }
        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public int ProgramBadCount { get; set; }

        
    }
}
