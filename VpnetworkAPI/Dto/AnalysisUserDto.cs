namespace VpnetworkAPI.Dto
{
    public class AnalysisUserDto
    {
        public Guid AnalysisId { get; set; }

        public string ProgramName { get; set; }
        public long MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public DateTime DateTime { get; set; }

        public string UserId { get; set; }




    }
}
