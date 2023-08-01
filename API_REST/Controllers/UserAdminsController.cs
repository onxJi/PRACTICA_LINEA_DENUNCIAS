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
    public class UserAdminsController : ControllerBase
    {
        private readonly LineaDenunciasDbContext _context;

        public UserAdminsController(LineaDenunciasDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAdmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdmin>>> GetUserAdmins()
        {
          if (_context.UserAdmins == null)
          {
              return NotFound();
          }
            return await _context.UserAdmins.ToListAsync();
        }

        [HttpGet("CheckLogin")]
        public async Task<ActionResult<bool>> CheckLogin(string user, string pass)
        {
            var validUser = await _context.UserAdmins.FirstOrDefaultAsync(x => x.Usuario == user && x.Pass == pass);
            if (validUser == null)
            {
                return BadRequest("Usuario o Contraseña incorrectos");
            }

            return validUser != null;
        }
        // GET: api/UserAdmins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdmin>> GetUserAdmin(long id)
        {
          if (_context.UserAdmins == null)
          {
              return NotFound();
          }
            var userAdmin = await _context.UserAdmins.FindAsync(id);

            if (userAdmin == null)
            {
                return NotFound();
            }

            return userAdmin;
        }

        // PUT: api/UserAdmins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAdmin(long id, UserAdmin userAdmin)
        {
            if (id != userAdmin.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(userAdmin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAdminExists(id))
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

        // POST: api/UserAdmins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAdmin>> PostUserAdmin(UserAdmin userAdmin)
        {
          if (_context.UserAdmins == null)
          {
              return Problem("Entity set 'LineaDenunciasDbContext.UserAdmins'  is null.");
          }
            _context.UserAdmins.Add(userAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAdmin", new { id = userAdmin.IdUsuario }, userAdmin);
        }

        // DELETE: api/UserAdmins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAdmin(long id)
        {
            if (_context.UserAdmins == null)
            {
                return NotFound();
            }
            var userAdmin = await _context.UserAdmins.FindAsync(id);
            if (userAdmin == null)
            {
                return NotFound();
            }

            _context.UserAdmins.Remove(userAdmin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAdminExists(long id)
        {
            return (_context.UserAdmins?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
