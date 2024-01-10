using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Linq;
using VpnetworkAPI.Models;
using VpnetworkAPI.DbContex; // Corrected namespace

namespace VpnetworkAPI.Services
{
    public class AnalysisRepository : InterfaceAnalysis
    {
        private readonly SieveProcessor _sieveProcessor;
        public readonly UserDbContext dbContex;

        public AnalysisRepository(SieveProcessor sieveProcessor, UserDbContext db)
        {
            dbContex = db;
            _sieveProcessor = sieveProcessor;
        }

        public IActionResult GetAnalysis(string userId, string programName, DateTime dateTime, long memoryUsage, double NetworkUsage, SieveModel sieveModel)
        {
            try
            {
                var analysisData = dbContex.Analyses
                    .Where(a => a.UserId == userId)
                    .AsQueryable();

                // Apply filtering using Sieve
                var filteredData = _sieveProcessor.Apply<Analysis>(sieveModel, analysisData)
                    .Where(a => a.ProgramName == programName &&
                                a.MemoryUsage == memoryUsage &&
                                a.NetworkUsage == NetworkUsage);

                // Return the result
                return new OkObjectResult(filteredData);
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Example: _logger.LogError(ex, "An error occurred while retrieving analysis data.");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        public IActionResult UpdateAnalysis(Guid analysisId, Analysis analysis)
        {
            try
            {
                var existingAnalysis = dbContex.Analyses.Find(analysisId);
                if (existingAnalysis == null)
                {
                    return new NotFoundObjectResult("Analysis not found.");
                }

                dbContex.SaveChanges();

                return new OkObjectResult("Analysis updated successfully");
            }
            catch (Exception ex)
            {
               
                return new StatusCodeResult(500); //
            }
        }

    }
}
