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
    public class CatalogosController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public CatalogosController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/Catalogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetCatalogos()
        {
          if (_context.Catalogos == null)
          {
              return NotFound();
          }
            return await _context.Catalogos.ToListAsync();
        }

        // GET: api/Catalogos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalogo>> GetCatalogo(long id)
        {
          if (_context.Catalogos == null)
          {
              return NotFound();
          }
            var catalogo = await _context.Catalogos.FindAsync(id);

            if (catalogo == null)
            {
                return NotFound();
            }

            return catalogo;
        }

        // PUT: api/Catalogos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogo(long id, Catalogo catalogo)
        {
            if (id != catalogo.IdDenuncia)
            {
                return BadRequest();
            }

            _context.Entry(catalogo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(id))
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

        // POST: api/Catalogos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catalogo>> PostCatalogo(Catalogo catalogo)
        {
          if (_context.Catalogos == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.Catalogos'  is null.");
          }
            _context.Catalogos.Add(catalogo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CatalogoExists(catalogo.IdDenuncia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCatalogo", new { id = catalogo.IdDenuncia }, catalogo);
        }

        // DELETE: api/Catalogos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(long id)
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoExists(long id)
        {
            return (_context.Catalogos?.Any(e => e.IdDenuncia == id)).GetValueOrDefault();
        }
    }
}
