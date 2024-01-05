using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;
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
        public async Task<ActionResult<IEnumerable<Analysis>>> GetAnalysesByUserId(string userId)
        {
            var analyses = await _context.Analyses
                .Where(a => a.UserId == userId)
                .ToListAsync();

            if (analyses == null || analyses.Count == 0)
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

        // PUT: api/Analyses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalysis(string id, [FromBody] Analysis analysis)
        {
            if (id != analysis.UserId)
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

        // DELETE: api/Analyses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(string id)
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

        private bool AnalysisExists(string id)
        {
            return _context.Analyses.Any(e => e.UserId == id);
        }
    }
}
