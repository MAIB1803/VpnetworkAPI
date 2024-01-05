using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Linq;
using System.Collections.Generic; // Add this for List<T>
using VpnetworkAPI.Models;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;

namespace VpnetworkAPI.Services
{
    public class AnalysisRepository : InterfaceAnalysis
    {
        private readonly SieveProcessor _sieveProcessor;
        public readonly UserDbContext dbContext;
        public AnalysisRepository(SieveProcessor sieveProcessor , UserDbContext db)
        {
            dbContext = db;
            _sieveProcessor = sieveProcessor;
    }

        public IActionResult GetAnalysis(string userId, string programName, DateTime dateTime, long memoryUsage, double NetworkUsage, SieveModel sieveModel)
        {
            try
            {
                // Your logic to get analysis data
                var analysisData = GetAnalysisData(userId);

                // Apply sorting and filtering using Sieve
                var filteredData = _sieveProcessor.Apply<Analysis>(sieveModel, analysisData)
                    .Where(a => a.UserId == userId &&
                                a.ProgramName == programName &&
                                a.DateTime == dateTime &&
                                a.MemoryUsage == memoryUsage &&
                                a.NetworkUsage == NetworkUsage);

                // Return the result
                // For example:
                return new OkObjectResult(filteredData);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return new StatusCodeResult(500); // Use StatusCodes.Status500InternalServerError if you prefer constants
            }
        }

        private IQueryable<Analysis> GetAnalysisData(string userId)
        {
            var analysisData = dbContext.Analyses
               .Where(a => a.UserId == userId)
               .AsQueryable(); // Return the data as an IQueryable

            return analysisData;
        }

        public IActionResult UpdateAnalysis(int UserId, Analysis analysis)
        {
            try
            {
                return new OkObjectResult("Analysis updated successfully");
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return new StatusCodeResult(500); // Use StatusCodes.Status500InternalServerError if you prefer constants
            }
        }
    }
}
