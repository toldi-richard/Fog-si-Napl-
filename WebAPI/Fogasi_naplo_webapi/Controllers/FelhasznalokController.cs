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
    public class FelhasznalokController : ControllerBase
    {
        private readonly FogasiNaploContext _context;

        public FelhasznalokController(FogasiNaploContext context)
        {
            _context = context;
        }

        // Csak a horgászok lekérdezésére, azok kilistázhatóságára
        // GET: api/Felhasznalok
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetUsers()
        {

            return await _context.felhasznalok.Where(x => x.szerepkorID != 1 && x.szerepkorID != 2).ToListAsync();
        }

        // Egy specifikus horgász lekérésére
        // GET: api/Felhasznalok/111111
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> GetUser(int id)
        {
            var user = await _context.felhasznalok.FindAsync(id);

            if (user == null || user.szerepkorID == 1 || user.szerepkorID == 2)
            {
                return NotFound();
            }

            return user;
        }


        // Halőrök lekérésére kellett a mobil profil oldalához
        // GET: api/Felhasznalok/Halorok/20000
        [HttpGet("Halorok/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> GetGuard(int id)
        {
            var user = await _context.felhasznalok.FindAsync(id);

            if (user == null || user.szerepkorID == 1 || user.szerepkorID == 3)
            {
                return NotFound();
            }

            return user;
        }

        // Összes felhasználó lekérése a weboldal admin felületéhez
        // GET: api/Felhasznalok
        [HttpGet("Mind")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetAllUsers()
        {

            return await _context.felhasznalok.ToListAsync();
        }


        // Mobil login funkciójához, belépés ellenőrzésére létrehozott végpont
        // JWT hiánya miatt a mobil belépést így tudtuk megoldani
        // Mobil nem támogatja egyelőre a hashelt jelszavakat és a tokeneket, ezért a halőr jelszava sincs hashelve
        // Jelszavakat a webapi nem küldi le, csak a beérkező adatokról dönti el, hogy az adatbázisban benne vannak-e
        // illetve, hogy azok megegyeznek-e
        // Ha az adatok egyeznek egy Ok response-t és a kért felhasználó azonosítóját egyeztetésre leküldi
        // hogy a mobil is tudja ellenőrizni a megadott azonosítóval egyezik-e

        // GET: api/Felhasznalok/Halor/azonosito/jelszo
        [HttpGet("Halor/{id}/{password}")]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> GetFishGuard(int id, string password)
        {
            var users = await _context.felhasznalok.SingleOrDefaultAsync(x => x.azonosito == id && x.szerepkorID == 2);

            if (users != null && password != null)
            {
                if (users.jelszo == password)
                {
                    return Ok(users.azonosito);
                }

                return NotFound();
            }
            else if (users == null)
            {
                return NotFound();
            }
            else
            {
                return Conflict();
            }
        }

        // GET: api/Felhasznalok/Azonosito/email
        [HttpGet("Azonosito/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> GetUserID(string email)
        {
            var user = await _context.felhasznalok.SingleOrDefaultAsync(x => x.email_cim == email);

            if (user != null)
            {return Ok(user.azonosito);}
            else 
                return NotFound();
        }

        // GET: api/Felhasznalok/Szerepkor/email
        [HttpGet("Szerepkor/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> GetUserRole(string email)
        {
            var user = await _context.felhasznalok.SingleOrDefaultAsync(x => x.email_cim == email);

            if (user != null)
            { return Ok(user.szerepkorID); }
            else
                return NotFound();
        }

        // PUT: api/Felhasznalok/111111
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutUser(int id, Felhasznalo user)
        {
            if (id != user.azonosito)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FelhasznaloExists(id))
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

        // POST: api/Felhasznalok
        // Overposting védelemmel ellátva: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Felhasznalo>> PostUser(Felhasznalo user)
        {
            _context.felhasznalok.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FelhasznaloExists(user.azonosito))
                {
                    return Conflict("Ezzel az azonositóval már létezik felhasználó.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFelhasznalo", new { id = user.azonosito }, user);
        }

        // DELETE: api/Felhasznalok/111111
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.felhasznalok.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.felhasznalok.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FelhasznaloExists(int id)
        {
            return _context.felhasznalok.Any(e => e.azonosito == id);
        }

        //GET: api/Felhasznalok/szama
        [HttpGet("szama")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Felhasznalo>>> GetNumberOfUsers()
        {
            int count = await  _context.felhasznalok.Where(x => x.szerepkorID != 1 && x.szerepkorID != 2).CountAsync();

            return Ok(count);
        }
    }
}
