//using Microsoft.ML.Data;
using System;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex; // Corrected namespace
using VpnetworkAPI.Dto;
using VpnetworkAPI.Models;
using VpnetworkAPI.Services;

namespace VpnetworkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysesController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly InterfaceAnalysis _services;

        public AnalysesController(UserDbContext context, InterfaceAnalysis _serv)
        {
            _context = context;
            _services = _serv;
        }

        // GET: api/Analyses
        [HttpGet("/api/GetAllAnalysisData")]
        public IActionResult  GetAnalyses()
        {
            var data = _services.GetAnalyses();

            //var data= await _context.Analyses.ToListAsync();
            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" ,data=data});
        }

        // GET: api/Analyses/user/{userId}
        [HttpGet("/api/GetAnalysesByUserId")]
        public IActionResult GetAnalysesByUserId(string userId) // Changed to int assuming UserId is int
        {
             var data=_services.GetAnalysesByUserId(userId);
               
            if(data!=null)
            {
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success", data = data });
            }
            else
            {
                return BadRequest(new { StatusCode = (int)HttpStatusCode.BadRequest, Message = " Data is not Found according UserId ", data = data });
            }
            
        }

        // POST: api/Analyses
        [HttpPost("/api/PostAnalysis")]
        public IActionResult PostAnalysis(string userId, [FromBody] AnalysesDto analysis)
        {
           
            _services.PostAnalysis(userId,analysis);
            if (null!=null)
            {
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" });
            }
            else
            {
                return BadRequest(new { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Who's present userid in User table and Who's present userid in Analysis table did not match.." });
            }
            

        }

        // PUT: api/Analyses/{guid}
        [HttpPut("{id:guid}")] // Modified to accept a GUID
        public async Task<IActionResult> PutAnalysis(Guid id, [FromBody] AnalysisUserDto analysis)
        {
            var updatedAnalysis = _services.PutAnalysis(id, analysis);

            if (updatedAnalysis == null)
            {
                return NotFound(); // Assuming you want to return NotFound if the item is not found
            }

            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" ,data= updatedAnalysis });
        }

        // DELETE: api/Analyses/{guid}
        [HttpDelete("{id:guid}")] // Modified to accept a GUID
        public async Task<IActionResult> DeleteAnalysis(Guid id)
        {


            var updatedAnalysis = _services.DeleteAnalysis(id);

            if (updatedAnalysis == null)
            {
                return BadRequest(new { StatusCode = (int)HttpStatusCode.BadRequest, Message = "Data is not deleted"});
            }
            else
            {
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success", data = updatedAnalysis });

            }
        }

        //private bool AnalysisExists(Guid id) // Method updated to check by GUID
        //{
        //    return _context.Analyses.Any(e => e.AnalysisId == id);
        //}

        
    }
}
