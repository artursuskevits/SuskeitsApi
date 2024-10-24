using Microsoft.AspNetCore.Mvc;
using SuskeitsApi.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private static List<Kasutaja> _tooted = new()
        {
            new Kasutaja(1, "KoolaLover", "123ABC", "Vladlen","Semenov"),
            new Kasutaja (2, "KoolaLover", "123ABC", "Vladlen", "Ivanov"),
            new Kasutaja (3, "KoolaLover", "123ABC", "Vladlen", "Oleksandrov"),
            new Kasutaja (4, "KoolaLover", "123ABC", "Vladlen", "olegov"),
            new Kasutaja (5, "KoolaLover", "123ABC", "Vladlen", "Mikitin")
        };

        // GET https://localhost:7198/tooted
        [HttpGet]
        public List<Kasutaja> Get()
        {
            return _tooted;
        }

        // DELETE https://localhost:7198/tooted/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Kasutaja> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        [HttpDelete("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index);
            return "Kustutatud!";
        }

        // POST https://localhost:7198/tooted/lisa/1/Coca/1.5/true
        [HttpPost("lisa/{id}/{nimickamee}/{parool}/{nimi}/{perenimi}")]
        public async Task<List<Kasutaja>> Add(int id, string nimickamee, string parool, string nimi, string perenimi)
        {
            Kasutaja kasutaja = new Kasutaja(id, nimickamee, parool, nimi, perenimi);

            // Add to the in-memory list
            _tooted.Add(kasutaja);

            // Insert into the XAMPP MySQL database
            await _context.Kasutajad.AddAsync(kasutaja);
            await _context.SaveChangesAsync();

            return _tooted;
        }


        [HttpPost("lisa2")]
        public List<Kasutaja> Add2(int id, string nimickamee, string parool, string nimi, string perenimi)
        {
            Kasutaja toode = new Kasutaja(id, nimickamee, parool, nimi, perenimi);
            _tooted.Add(toode);
            return _tooted;
        }
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
