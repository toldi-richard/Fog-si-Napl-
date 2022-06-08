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
    public class HelyszinekController : ControllerBase
    {
        private readonly FogasiNaploContext _context;

        public HelyszinekController(FogasiNaploContext context)
        {
            _context = context;
        }

        // GET: api/Helyszinek
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Helyszin>>> GetPlaces()
        {
            return await _context.helyszinek.ToListAsync();
        }

        // GET: api/Helyszinek/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Helyszin>> GetPlace(int id)
        {
            var place = await _context.helyszinek.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // PUT: api/Helyszinek/5
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutPlace(int id, Helyszin place)
        {
            if (id != place.helyszinID)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelyszinExists(id))
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

        // POST: api/Helyszinek
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Helyszin>> PostPlace(Helyszin place)
        {
            _context.helyszinek.Add(place);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HelyszinExists(place.helyszinID))
                {
                    return Conflict("Ezzel az ID-val már létezik helyszín.");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetHelyszin", new { id = place.helyszinID }, place);
        }

        // DELETE: api/Helyszinek/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.helyszinek.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.helyszinek.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HelyszinExists(int id)
        {
            return _context.helyszinek.Any(e => e.helyszinID == id);
        }
    }
}
