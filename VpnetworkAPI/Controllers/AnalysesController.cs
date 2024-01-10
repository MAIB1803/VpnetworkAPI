//using Microsoft.ML.Data;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex; // Corrected namespace
using VpnetworkAPI.Models;

namespace VpnetworkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysesController : ControllerBase
    {
        private readonly UserDbContext _context;

        public AnalysesController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Analyses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analysis>>> GetAnalyses()
        {
            return await _context.Analyses.ToListAsync();
        }

        // GET: api/Analyses/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Analysis>>> GetAnalysesByUserId(string userId) // Changed to int assuming UserId is int
        {
            var analyses = await _context.Analyses
                .Where(a => a.UserId == userId)
                .ToListAsync();

            if (analyses == null)
            {
                return NotFound("No analysis data found for the specified user.");
            }

            return analyses;
        }

        // POST: api/Analyses
        [HttpPost]
        public async Task<IActionResult> PostAnalysis([FromBody] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                _context.Analyses.Add(analysis);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAnalysesByUserId), new { userId = analysis.UserId }, analysis);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Analyses/{guid}
        [HttpPut("{id:guid}")] // Modified to accept a GUID
        public async Task<IActionResult> PutAnalysis(Guid id, [FromBody] Analysis analysis)
        {
            if (id != analysis.AnalysisId) // Check against AnalysisId, which is a GUID
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(analysis).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalysisExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Analyses/{guid}
        [HttpDelete("{id:guid}")] // Modified to accept a GUID
        public async Task<IActionResult> DeleteAnalysis(Guid id)
        {
            var analysis = await _context.Analyses.FindAsync(id);
            if (analysis == null)
            {
                return NotFound();
            }

            _context.Analyses.Remove(analysis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnalysisExists(Guid id) // Method updated to check by GUID
        {
            return _context.Analyses.Any(e => e.AnalysisId == id);
        }

        
    }
}
