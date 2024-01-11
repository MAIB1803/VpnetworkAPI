using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Linq;
using VpnetworkAPI.Models;
using VpnetworkAPI.DbContex;
using VpnetworkAPI.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc; // Corrected namespace

namespace VpnetworkAPI.Services
{
    public class AnalysisRepository : InterfaceAnalysis
    {
        private readonly SieveProcessor _sieveProcessor;
        public readonly UserDbContext dbContex;
       
        public readonly IMapper _map;

        public AnalysisRepository(SieveProcessor sieveProcessor, UserDbContext db, IMapper map)
        {
            dbContex = db;
            _sieveProcessor = sieveProcessor;
          
            _map = map;

        }

        public List<AnalysisUserDto> GetAnalyses()
        {
            var result=dbContex.Analyses.Include(t=>t.User).ToList();

            try
            {
                var data = _map.Map<List<AnalysisUserDto>>(result);
                return data;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            

        }


        public AnalysisUserDto GetAnalysesByUserId(string userId)
        {
            var data = dbContex.Analyses.Where(u => u.UserId == userId).FirstOrDefault();
            try
            {
                var result = _map.Map<AnalysisUserDto>(data);
                return result;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }


        public AnalysisUserDto PutAnalysis(Guid id, AnalysisUserDto analysis)
        {
            var existingAnalysis = dbContex.Analyses.Find(id);
            if (existingAnalysis == null)
            {
                return null; // Or throw an exception, depending on your requirements
            }
            existingAnalysis.ProgramName = analysis.ProgramName;
            existingAnalysis.MemoryUsage = analysis.MemoryUsage;
            existingAnalysis.NetworkUsage = analysis.NetworkUsage;
            existingAnalysis.DateTime = analysis.DateTime;

            dbContex.SaveChanges();

            return _map.Map<AnalysisUserDto>(existingAnalysis);
        }


        public AnalysisUserDto DeleteAnalysis(Guid id)
        {
            var existingAnalysis = dbContex.Analyses.Find(id);

            if (existingAnalysis == null)
            {
                return null; // Or throw an exception, depending on your requirements
            }

            var deletedDto = _map.Map<AnalysisUserDto>(existingAnalysis);

            dbContex.Analyses.Remove(existingAnalysis);
            dbContex.SaveChanges();

            return deletedDto;
        }

        //public IActionResult GetAnalysis(string userId, string programName, DateTime dateTime, long memoryUsage, double NetworkUsage, SieveModel sieveModel)
        //{
        //    try
        //    {
        //        var analysisData = dbContex.Analyses.Include(ut=>ut.User)
        //            .Where(a => a.UserId == userId)
        //            .AsQueryable();

        //        // Apply filtering using Sieve
        //        var filteredData = _sieveProcessor.Apply<Analysis>(sieveModel, analysisData)
        //            .Where(a => a.ProgramName == programName &&
        //                        a.MemoryUsage == memoryUsage &&
        //                        a.NetworkUsage == NetworkUsage);

        //        // Return the result
        //        return new OkObjectResult(filteredData);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        // Example: _logger.LogError(ex, "An error occurred while retrieving analysis data.");
        //        return new StatusCodeResult(500); // Internal Server Error
        //    }
        //}

        //public IActionResult UpdateAnalysis(Guid analysisId, Analysis analysis)
        //{
        //    try
        //    {
        //        var existingAnalysis = dbContex.Analyses.Find(analysisId);
        //        if (existingAnalysis == null)
        //        {
        //            return new NotFoundObjectResult("Analysis not found.");
        //        }

        //        dbContex.SaveChanges();

        //        return new OkObjectResult("Analysis updated successfully");
        //    }
        //    catch (Exception ex)
        //    {

        //        return new StatusCodeResult(500); //
        //    }
        //}

        //Add Analysis Code by repository

        public Analysis PostAnalysis(string userId,AnalysesDto analysis)
        {
           
            var userIdIsPresentUserTable = dbContex.Users.FirstOrDefault(ut => ut.UserId== userId);

            if (userIdIsPresentUserTable!=null)
            {
                var data = _map.Map<Analysis>(analysis);
                var Results = dbContex.Analyses.Add(data);
                dbContex.SaveChanges();
                return Results.Entity;

            }
            else
            {
                return null;   
            }
        }

    }
}
