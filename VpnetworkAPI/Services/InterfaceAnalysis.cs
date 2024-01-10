using Microsoft.AspNetCore.Mvc;
using VpnetworkAPI.Models;
using Sieve.Models;

namespace VpnetworkAPI.Services
{
    public interface InterfaceAnalysis
    {
        IActionResult GetAnalysis(string userId, string programName, DateTime dateTime, long memoryUsage, double NetworkUsage, SieveModel sieveModel);


    }
}
