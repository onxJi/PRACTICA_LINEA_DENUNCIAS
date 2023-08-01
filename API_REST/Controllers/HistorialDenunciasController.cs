using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST.Models;
using Microsoft.VisualBasic;

namespace API_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialDenunciasController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public HistorialDenunciasController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialDenuncias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialDenuncia>>> GetHistorialDenuncia()
        {
          if (_context.HistorialDenuncia == null)
          {
              return NotFound();
          }
            return await _context.HistorialDenuncia.ToListAsync();
        }

        // GET: api/HistorialDenuncias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialDenuncia>> GetHistorialDenuncia(long id)
        {
          if (_context.HistorialDenuncia == null)
          {
              return NotFound();
          }
            var historialDenuncia = await _context.HistorialDenuncia.FindAsync(id);

            if (historialDenuncia == null)
            {
                return NotFound();
            }

            return historialDenuncia;
        }
        [HttpGet("ObtenerHistorial/{id}")]
        public async Task<ActionResult<List<HistorialDenuncia>>> GetDatosPersonales(long id)
        {
            if (_context.DatosPersonales == null)
            {
                return NotFound();
            }
            var historialDenuncias = await _context.HistorialDenuncia
                                        .Where(denuncia => denuncia.IdDenuncia == id)
                                        .ToListAsync();
            if (historialDenuncias == null)
            {
                return NotFound();
            }

            return historialDenuncias;
        }

        // PUT: api/HistorialDenuncias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialDenuncia(long id, HistorialDenuncia historialDenuncia)
        {
            if (id != historialDenuncia.IdHistorial)
            {
                return BadRequest();
            }

            _context.Entry(historialDenuncia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialDenunciaExists(id))
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

        // POST: api/HistorialDenuncias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialDenuncia>> PostHistorialDenuncia(HistorialDenuncia historialDenuncia)
        {
          if (_context.HistorialDenuncia == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.HistorialDenuncia'  is null.");
          }
            _context.HistorialDenuncia.Add(historialDenuncia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialDenuncia", new { id = historialDenuncia.IdHistorial }, historialDenuncia);
        }

        // DELETE: api/HistorialDenuncias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialDenuncia(long id)
        {
            if (_context.HistorialDenuncia == null)
            {
                return NotFound();
            }
            var historialDenuncia = await _context.HistorialDenuncia.FindAsync(id);
            if (historialDenuncia == null)
            {
                return NotFound();
            }

            _context.HistorialDenuncia.Remove(historialDenuncia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialDenunciaExists(long id)
        {
            return (_context.HistorialDenuncia?.Any(e => e.IdHistorial == id)).GetValueOrDefault();
        }
    }
}
