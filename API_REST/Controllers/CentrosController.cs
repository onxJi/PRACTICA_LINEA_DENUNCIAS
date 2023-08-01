using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST.Models;

namespace API_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentrosController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public CentrosController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/Centros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Centro>>> GetCentros()
        {
          if (_context.Centros == null)
          {
              return NotFound();
          }
            return await _context.Centros.ToListAsync();
        }

        // GET: api/Centros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Centro>> GetCentro(long id)
        {
          if (_context.Centros == null)
          {
              return NotFound();
          }
            var centro = await _context.Centros.FindAsync(id);

            if (centro == null)
            {
                return NotFound();
            }

            return centro;
        }

        // PUT: api/Centros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCentro(long id, Centro centro)
        {
            if (id != centro.IdCentro)
            {
                return BadRequest();
            }

            _context.Entry(centro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroExists(id))
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

        // POST: api/Centros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Centro>> PostCentro(Centro centro)
        {
          if (_context.Centros == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.Centros'  is null.");
          }
            _context.Centros.Add(centro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCentro", new { id = centro.IdCentro }, centro);
        }

        // DELETE: api/Centros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentro(long id)
        {
            if (_context.Centros == null)
            {
                return NotFound();
            }
            var centro = await _context.Centros.FindAsync(id);
            if (centro == null)
            {
                return NotFound();
            }

            _context.Centros.Remove(centro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CentroExists(long id)
        {
            return (_context.Centros?.Any(e => e.IdCentro == id)).GetValueOrDefault();
        }
    }
}
