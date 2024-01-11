using Microsoft.VisualBasic;

namespace VpnetworkAPI.Dto
{
    public class AnalysesDto
    {
        public Guid  AnalysesId { get; set; }
        
        public string ProgramName { get; set; }
        public long MemoryStorage { get; set; }
        public double NetwaorkUsage { get; set; }
        public DateTime dateTime { get; set; }
    }
}
