using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fogasi_naplo_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "adminisztrator")]
    public class SzerepkorokController : ControllerBase
    {
        private readonly FogasiNaploContext _context;

        public SzerepkorokController(FogasiNaploContext context)
        {
            _context = context;
        }

        // GET: api/Szerepkorok
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Szerepkor>>> GetRoles()
        {
            return await _context.szerepkorok.ToListAsync();
        }

        // GET: api/Szerepkorok/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Szerepkor>> GetRole(int id)
        {
            var role = await _context.szerepkorok.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Szerepkorok/5
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutRole(int id, Szerepkor role)
        {
            if (id != role.szerepkorID)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SzerepkorExists(id))
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

        // POST: api/Szerepkorok
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Szerepkor>> PostRole(Szerepkor role)
        {
            _context.szerepkorok.Add(role);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SzerepkorExists(role.szerepkorID))
                {
                    return Conflict("Ezzel az ID-val már létezik szerepkor.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSzerepkor", new { id = role.szerepkorID }, role);
        }

        // DELETE: api/Szerepkorok/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.szerepkorok.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.szerepkorok.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SzerepkorExists(int id)
        {
            return _context.szerepkorok.Any(e => e.szerepkorID == id);
        }
    }
}
