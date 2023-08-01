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
    public class UserDenunciantesController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public UserDenunciantesController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/UserDenunciantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDenunciante>>> GetUserDenunciantes()
        {
          if (_context.UserDenunciantes == null)
          {
              return NotFound();
          }
            return await _context.UserDenunciantes.ToListAsync();
        }

        // GET: api/UserDenunciantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDenunciante>> GetUserDenunciante(long id)
        {
          if (_context.UserDenunciantes == null)
          {
              return NotFound();
          }
            var userDenunciante = await _context.UserDenunciantes.FindAsync(id);

            if (userDenunciante == null)
            {
                return NotFound();
            }

            return userDenunciante;
        }
        [HttpGet("CheckLogin")]
        public async Task<ActionResult<bool>> CheckLogin(int user, string pass)
        {
            var validUser = await _context.UserDenunciantes.FirstOrDefaultAsync(
                x => x.Folio == user && x.PasswordDenuncia == pass);
            if (validUser == null)
            {
                return BadRequest("Usuario o Contraseña incorrectos");
            }

            return validUser != null;
        }
        // PUT: api/UserDenunciantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDenunciante(long id, UserDenunciante userDenunciante)
        {
            if (id != userDenunciante.IdDenunciante)
            {
                return BadRequest();
            }

            _context.Entry(userDenunciante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDenuncianteExists(id))
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

        // POST: api/UserDenunciantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDenunciante>> PostUserDenunciante(UserDenunciante userDenunciante)
        {
          if (_context.UserDenunciantes == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.UserDenunciantes'  is null.");
          }
            _context.UserDenunciantes.Add(userDenunciante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDenunciante", new { id = userDenunciante.IdDenunciante }, userDenunciante);
        }

        // DELETE: api/UserDenunciantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDenunciante(long id)
        {
            if (_context.UserDenunciantes == null)
            {
                return NotFound();
            }
            var userDenunciante = await _context.UserDenunciantes.FindAsync(id);
            if (userDenunciante == null)
            {
                return NotFound();
            }

            _context.UserDenunciantes.Remove(userDenunciante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDenuncianteExists(long id)
        {
            return (_context.UserDenunciantes?.Any(e => e.IdDenunciante == id)).GetValueOrDefault();
        }
    }
}
