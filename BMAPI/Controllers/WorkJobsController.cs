using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Classes.Context;
using Classes.Entities.JoinEntities;

namespace BMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkJobsController : ControllerBase
    {
        private readonly BMSDBContext _context;

        public WorkJobsController(BMSDBContext context)
        {
            _context = context;
        }

        // GET: api/WorkJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkJobs>>> GetWorkJobs()
        {
          if (_context.WorkJobs == null)
          {
              return NotFound();
          }
            return await _context.WorkJobs.ToListAsync();
        }

        // GET: api/WorkJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkJobs>> GetWorkJobs(int id)
        {
          if (_context.WorkJobs == null)
          {
              return NotFound();
          }
            var workJobs = await _context.WorkJobs.FindAsync(id);

            if (workJobs == null)
            {
                return NotFound();
            }

            return workJobs;
        }

        // PUT: api/WorkJobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkJobs(int id, WorkJobs workJobs)
        {
            if (id != workJobs.Id)
            {
                return BadRequest();
            }

            _context.Entry(workJobs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkJobsExists(id))
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

        // POST: api/WorkJobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkJobs>> PostWorkJobs(WorkJobs workJobs)
        {
          if (_context.WorkJobs == null)
          {
              return Problem("Entity set 'BMSDBContext.WorkJobs'  is null.");
          }
            _context.WorkJobs.Add(workJobs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkJobs", new { id = workJobs.Id }, workJobs);
        }

        // DELETE: api/WorkJobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkJobs(int id)
        {
            if (_context.WorkJobs == null)
            {
                return NotFound();
            }
            var workJobs = await _context.WorkJobs.FindAsync(id);
            if (workJobs == null)
            {
                return NotFound();
            }

            _context.WorkJobs.Remove(workJobs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkJobsExists(int id)
        {
            return (_context.WorkJobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
