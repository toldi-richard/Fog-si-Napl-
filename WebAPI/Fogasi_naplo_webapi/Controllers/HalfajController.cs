using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fogasi_naplo_webapi.Controllers
{
    // Adatbázis nélküli egység teszteknek létrehozott Controller
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "adminisztrator")]
    public class HalfajController : ControllerBase
    {
        List<Halfaj> fishes = new List<Halfaj>();

        public HalfajController(List<Halfaj> fishes)
        {
            this.fishes = fishes;
        }

        // GET: api/Halfaj
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Halfaj> GetAllFishes()
        {
            return fishes;
        }

        // GET: api/Halfaj/Async
        [HttpGet("Async")]
        [AllowAnonymous]
        public async Task<IEnumerable<Halfaj>> GetAllFishesAsync()
        {
            return await Task.FromResult(GetAllFishes());
        }

        // GET: api/Halfaj/1
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetFish(int id)
        {
            var fish = fishes.FirstOrDefault((p) => p.Id == id);
            if (fish == null)
            {
                return NotFound();
            }
            return Ok(fish);
        }

        // GET: api/Halfaj/id/1
        [HttpGet("id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFishAsync(int id)
        {
            return await Task.FromResult(GetFish(id));
        }
    }
}
