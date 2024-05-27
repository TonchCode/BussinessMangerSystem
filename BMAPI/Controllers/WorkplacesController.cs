using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Classes.Context;
using Classes.Entities;

namespace BMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : ControllerBase
    {
        private readonly BMSDBContext _context;

        public WorkplacesController(BMSDBContext context)
        {
            _context = context;
        }

        // GET: api/Workplaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workplace>>> GetWorkplaces()
        {
          if (_context.Workplaces == null)
          {
              return NotFound();
          }
            return await _context.Workplaces.ToListAsync();
        }

        // GET: api/Workplaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workplace>> GetWorkplace(int id)
        {
          if (_context.Workplaces == null)
          {
              return NotFound();
          }
            var workplace = await _context.Workplaces.FindAsync(id);

            if (workplace == null)
            {
                return NotFound();
            }

            return workplace;
        }

        // PUT: api/Workplaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkplace(int id, Workplace workplace)
        {
            if (id != workplace.Id)
            {
                return BadRequest();
            }

            _context.Entry(workplace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkplaceExists(id))
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

        // POST: api/Workplaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workplace>> PostWorkplace(Workplace workplace)
        {
          if (_context.Workplaces == null)
          {
              return Problem("Entity set 'BMSDBContext.Workplaces'  is null.");
          }
            _context.Workplaces.Add(workplace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkplace", new { id = workplace.Id }, workplace);
        }

        // DELETE: api/Workplaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkplace(int id)
        {
            if (_context.Workplaces == null)
            {
                return NotFound();
            }
            var workplace = await _context.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return NotFound();
            }

            _context.Workplaces.Remove(workplace);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkplaceExists(int id)
        {
            return (_context.Workplaces?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
