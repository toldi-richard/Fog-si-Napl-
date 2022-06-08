using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Fogasi_naplo_webapi.DTOs;

namespace Fogasi_naplo_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "adminisztrator")]
    public class FogasokController : ControllerBase
    {
        private readonly FogasiNaploContext _context;

        public FogasokController(FogasiNaploContext context)
        {
            _context = context;
        }

        // Átalakító metódus
        private FogasokDTO ConvertToDTO(Fogas fogas)
        {
            if (fogas == null)
            {
                return null;
            }
            return new FogasokDTO(

                fogas.fogasID,
                fogas.azonosito,
                fogas.helyszin.vizterulet_neve,
                fogas.horgaszhely,
                fogas.datum_idopont.ToString("yyyy-MM-dd hh:mm"),
                fogas.halfaj,
                fogas.suly);
        }

        // GET: api/Fogasok/0 (összes lekérésére)
        // GET: api/Fogasok/111111 (egy felhasználó fogásainak lekérésére)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FogasokDTO>>> GetCatches(int id)
        {
            // TODO: Orderby fogásokat dátum szerinti sorrendbe adja vissza
            if (id == 0)
            {

                var fogasok = await _context.fogasok
                    .Include(x => x.helyszin)
                    .OrderByDescending(x=>x.datum_idopont)
                    .ToListAsync();

                return fogasok.Select(x => ConvertToDTO(x)).ToList();
            }
            else
            {
                var catchesOfOneUser = await _context.fogasok
                    .Include(x => x.helyszin)
                    .Where(x => x.azonosito == id)
                    .OrderByDescending(x => x.datum_idopont)
                    .ToListAsync();

                if (catchesOfOneUser == null)
                {
                    return NotFound();
                }
                else
                {
                    return catchesOfOneUser.Select(x => ConvertToDTO(x)).ToList();
                }
            }
        }

        // GET: api/Fogasok/fogas/5
        [HttpGet("fogas/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Fogas>> GetCatch(int id)
        {
            var fishCatch = await _context.fogasok.FindAsync(id);

            if (fishCatch == null)
            {
                return NotFound();
            }

            return fishCatch;
        }

        // PUT: api/Fogasok/5
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutCatch(int id, Fogas fishCatch)
        {
            if (id != fishCatch.fogasID)
            {
                return BadRequest();
            }

            _context.Entry(fishCatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FogasExists(id))
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

        // POST: api/Fogasok
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<Fogas>> PostCatch(Fogas fishCatch)
        {
            _context.fogasok.Add(fishCatch);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FogasExists(fishCatch.fogasID))
                {
                    return Conflict("Ezzel az ID-val már létezik fogás.");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetFogas", new { id = fishCatch.fogasID }, fishCatch);
        }

        // DELETE: api/Fogasok/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCatch(int id)
        {
            var fishCatch = await _context.fogasok.FindAsync(id);
            if (fishCatch == null)
            {
                return NotFound();
            }

            _context.fogasok.Remove(fishCatch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FogasExists(int id)
        {
            return _context.fogasok.Any(e => e.fogasID == id);
        }

        // GET: api/Fogasok/kifogotthal_szama/0 (mindenkire)
        // GET: api/Fogasok/kifogotthal_szama/111111 (azonosító) egy horgász esetén
        [HttpGet("kifogotthal_szama/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetNumberOfCaughtFish(int id)
        {
            if (id == 0)
            {
                float weightOfCaughtFishes = (float)_context.fogasok.Count();

                if (weightOfCaughtFishes == 0)
                {
                    return NotFound();
                }

                return Ok(weightOfCaughtFishes);
            }
            else
            {
                float numberOfCaughtFishes = (float)_context.fogasok.Where(x => x.azonosito == id).Count();

                if (numberOfCaughtFishes == 0)
                {
                    return NotFound();
                }

                return Ok(numberOfCaughtFishes);
            }
        }

        // GET: api/Fogasok/leggyakrabban_kifogott_halfaj/0 (mindenkire)
        // GET: api/Fogasok/leggyakrabban_kifogott_halfaj/111111 (azonosítóval) egy horgász esetén
        [HttpGet("leggyakrabban_kifogott_halfaj/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetMostFrequentCaughtFish(int id)
        {
            if (id == 0)
            {
                var mostCommonlyCaughtFish = await _context.fogasok.GroupBy(q => q.halfaj)
                                                    .OrderByDescending(gp => gp.Count())
                                                    .Take(1)
                                                    .Select(g => g.Key).ToListAsync();

                if (mostCommonlyCaughtFish == null)
                {
                    return NotFound();
                }

                return Ok(mostCommonlyCaughtFish);
            }
            else
            {
                var mostCommonlyCaughtFish = await _context.fogasok.Where(x => x.azonosito == id)
                                                    .GroupBy(q => q.halfaj)
                                                    .OrderByDescending(gp => gp.Count())
                                                    .Take(1)
                                                    .Select(g => g.Key).ToListAsync();

                if (mostCommonlyCaughtFish == null)
                {
                    return NotFound();
                }

                return Ok(mostCommonlyCaughtFish);
            }
        }

        // GET: api/Fogasok/kifogotthal_sulya/0 (mindenkire)
        // GET: api/Fogasok/kifogotthal_sulya/111111 (azonosítóval) egy horgász esetén
        [HttpGet("kifogotthal_sulya/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetWeightOfCaughtFish(int id)
        {
            if (id == 0)
            {
                float weightOfCaughtFishes = (float)_context.fogasok.Sum(x => x.suly);

                if (weightOfCaughtFishes == 0)
                {
                    return NotFound();
                }

                return Ok(weightOfCaughtFishes);
            }
            else
            {
                float weightOfCaughtFishes = (float)_context.fogasok.Where(x => x.azonosito == id).Sum(x => x.suly);

                if (weightOfCaughtFishes == 0)
                {
                    return NotFound();
                }

                return Ok(weightOfCaughtFishes);
            }
        }
    }
}
