using Microsoft.AspNetCore.Mvc;
using VpnetworkAPI.Models;
using Sieve.Models;
using VpnetworkAPI.Dto;

namespace VpnetworkAPI.Services
{
    public interface InterfaceAnalysis
    {
       //public IActionResult GetAnalysis(string userId, string programName, DateTime dateTime, long memoryUsage, double NetworkUsage, SieveModel sieveModel);
        
        public AnalysisUserDto GetAnalysesByUserId(string userId);

        public List<AnalysisUserDto> GetAnalyses();
        public Analysis PostAnalysis(string userId, AnalysesDto analysis);
        public AnalysisUserDto PutAnalysis(Guid id, AnalysisUserDto analysis);
        public AnalysisUserDto DeleteAnalysis(Guid id);

    }
}
