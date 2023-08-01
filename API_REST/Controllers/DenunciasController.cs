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
    public class DenunciasController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public DenunciasController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/Denuncias
        [HttpGet]
        public async Task<ActionResult<List<Denuncia>>> GetDenuncias()
        {
          if (_context.Denuncias == null)
          {
              return NotFound();
          }
            return await _context.Denuncias.ToListAsync();
        }

        // GET: api/Denuncias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Denuncia>> GetDenuncia(int id)
        {
          if (_context.Denuncias == null)
          {
              return NotFound();
          }
            var denuncia = await _context.Denuncias.FirstOrDefaultAsync(x => x.Folio == id);

            if (denuncia == null)
            {
                return NotFound();
            }

            return denuncia;
        }

        // PUT: api/Denuncias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDenuncia(long id, Denuncia denuncia)
        {
            if (id != denuncia.Folio)
            {
                return BadRequest();
            }

            _context.Entry(denuncia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DenunciaExists(id))
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
        // PUT: api/Denuncias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> PutDenunciaByFolio(long id, string status)
        {
            var existingDenuncia = await _context.Denuncias.FindAsync(id);
            if (existingDenuncia == null)
            {
                return NotFound();
            }

            existingDenuncia.Estatus = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DenunciaExists(id))
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

        // POST: api/Denuncias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Denuncia>> PostDenuncia(Denuncia denuncia)
        {
          if (_context.Denuncias == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.Denuncias'  is null.");
          }
            _context.Denuncias.Add(denuncia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DenunciaExists(denuncia.Folio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDenuncia", new { id = denuncia.Folio }, denuncia);
        }

        // DELETE: api/Denuncias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDenuncia(long id)
        {
            if (_context.Denuncias == null)
            {
                return NotFound();
            }
            var denuncia = await _context.Denuncias.FindAsync(id);
            if (denuncia == null)
            {
                return NotFound();
            }

            _context.Denuncias.Remove(denuncia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DenunciaExists(long id)
        {
            return (_context.Denuncias?.Any(e => e.Folio == id)).GetValueOrDefault();
        }
    }
}
