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
    public class DatosPersonalesController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public DatosPersonalesController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/DatosPersonales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatosPersonal>>> GetDatosPersonales()
        {
          if (_context.DatosPersonales == null)
          {
              return NotFound();
          }
            return await _context.DatosPersonales.ToListAsync();
        }

        // GET: api/DatosPersonales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DatosPersonal>> GetDatosPersonal(long id)
        {
          if (_context.DatosPersonales == null)
          {
              return NotFound();
          }
            var datosPersonal = await _context.DatosPersonales.FindAsync(id);

            if (datosPersonal == null)
            {
                return NotFound();
            }

            return datosPersonal;
        }
        // GET: api/DatosPersonales/ObtenerDatos
        [HttpGet("ObtenerDatos/{id}")]
        public async Task<ActionResult<DatosPersonal>> GetDatosPersonales(long id)
        {
            if (_context.DatosPersonales == null)
            {
                return NotFound();
            }
            var datosPersonal = await _context.DatosPersonales.FirstOrDefaultAsync(x => x.IdDenuncia == id);

            if (datosPersonal == null)
            {
                return NotFound();
            }

            return datosPersonal;
        }
        // PUT: api/DatosPersonales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatosPersonal(long id, DatosPersonal datosPersonal)
        {
            if (id != datosPersonal.IdDenunciante)
            {
                return BadRequest();
            }

            _context.Entry(datosPersonal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatosPersonalExists(id))
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

        // POST: api/DatosPersonales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DatosPersonal>> PostDatosPersonal(DatosPersonal datosPersonal)
        {
          if (_context.DatosPersonales == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.DatosPersonales'  is null.");
          }
            _context.DatosPersonales.Add(datosPersonal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDatosPersonal", new { id = datosPersonal.IdDenunciante }, datosPersonal);
        }

        // DELETE: api/DatosPersonales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatosPersonal(long id)
        {
            if (_context.DatosPersonales == null)
            {
                return NotFound();
            }
            var datosPersonal = await _context.DatosPersonales.FindAsync(id);
            if (datosPersonal == null)
            {
                return NotFound();
            }

            _context.DatosPersonales.Remove(datosPersonal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DatosPersonalExists(long id)
        {
            return (_context.DatosPersonales?.Any(e => e.IdDenunciante == id)).GetValueOrDefault();
        }
    }
}
