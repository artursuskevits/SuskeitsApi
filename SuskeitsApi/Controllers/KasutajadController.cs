using Microsoft.AspNetCore.Mvc;
using SuskeitsApi.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuskeitsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KasutajadController : Controller
    {
        private readonly SuskeitsDbContext _context;
        private readonly HttpClient _httpClient;

        public KasutajadController(HttpClient httpClient, SuskeitsDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // GET https://localhost:7198/kasutajad
        [HttpGet]
        public async Task<List<Kasutaja>> Get()
        {
            return await _context.Kasutajad.ToListAsync(); // Fetch users from database
        }

        // DELETE https://localhost:7198/kasutajad/kustuta/{index}
        [HttpDelete("kustuta/{index}")]
        public async Task<IActionResult> Delete(int index)
        {
            var kasutaja = await _context.Kasutajad.FindAsync(index);
            if (kasutaja == null)
            {
                return NotFound("User not found.");
            }

            _context.Kasutajad.Remove(kasutaja);
            await _context.SaveChangesAsync();
            return Ok(await _context.Kasutajad.ToListAsync());
        }

        [HttpDelete("kustuta2/{index}")]
        public async Task<IActionResult> Delete2(int index)
        {
            var kasutaja = await _context.Kasutajad.FindAsync(index);
            if (kasutaja == null)
            {
                return NotFound("User not found.");
            }

            _context.Kasutajad.Remove(kasutaja);
            await _context.SaveChangesAsync();
            return Ok("Kustutatud!");
        }

        // POST https://localhost:7198/kasutajad/lisa/{id}/{nimickamee}/{parool}/{nimi}/{perenimi}
        [HttpPost("lisa/{id}/{nimickamee}/{parool}/{nimi}/{perenimi}")]
        public async Task<List<Kasutaja>> Add(int id, string nimickamee, string parool, string nimi, string perenimi)
        {
            var kasutaja = new Kasutaja(id, nimickamee, parool, nimi, perenimi);
            await _context.Kasutajad.AddAsync(kasutaja);
            await _context.SaveChangesAsync();

            return await _context.Kasutajad.ToListAsync(); // Return updated list from database
        }

        // Optional additional post method
        [HttpPost("lisa2")]
        public async Task<List<Kasutaja>> Add2(int id, string nimickamee, string parool, string nimi, string perenimi)
        {
            var kasutaja = new Kasutaja(id, nimickamee, parool, nimi, perenimi);
            await _context.Kasutajad.AddAsync(kasutaja);
            await _context.SaveChangesAsync();

            return await _context.Kasutajad.ToListAsync();
        }

        // This method retrieves products from another API endpoint
        [HttpGet("tooted")]
        public async Task<List<Toode>> GetTootedFromAnotherApi()
        {
            var response = await _httpClient.GetAsync("https://localhost:7227/Tooted");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tooted = JsonSerializer.Deserialize<List<Toode>>(jsonResponse);

            return tooted;
        }
    }
}
