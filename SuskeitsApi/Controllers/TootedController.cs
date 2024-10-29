using Microsoft.AspNetCore.Mvc;
using SuskeitsApi.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SuskeitsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private readonly SuskeitsDbContext _context;

        public TootedController(SuskeitsDbContext context)
        {
            _context = context;
        }

        // GET https://localhost:7198/Tooted/get-users/{productId}
        [HttpGet("get-users/{productId}")]
        public async Task<IActionResult> GetUsersForProduct(int productId)
        {
            var toode = await _context.Tooted
                .Include(t => t.KasutajaTooted) // Include users linked through the join entity
                    .ThenInclude(kt => kt.Kasutaja)
                .FirstOrDefaultAsync(t => t.Id == productId);

            if (toode == null)
            {
                return NotFound("Toode not found.");
            }

            var users = toode.KasutajaTooted.Select(kt => kt.Kasutaja).ToList();
            return Ok(users);
        }

        // POST https://localhost:7198/Tooted/add
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(string name, double price, bool isActive, List<int> kasutajaIds)
        {
            var toode = new Toode
            {
                Name = name,
                Price = price,
                IsActive = isActive
            };

            _context.Tooted.Add(toode);
            await _context.SaveChangesAsync();

            // Link the new product to the provided users
            foreach (var kasutajaId in kasutajaIds)
            {
                var kasutaja = await _context.Kasutajad.FindAsync(kasutajaId);
                if (kasutaja != null)
                {
                    _context.KasutajaTooded.Add(new KasutajaToode
                    {
                        KasutajaId = kasutajaId,
                        ToodeId = toode.Id
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(toode);
        }

        // DELETE https://localhost:7198/Tooted/delete/{productId}
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var toode = await _context.Tooted
                .Include(t => t.KasutajaTooted)
                .FirstOrDefaultAsync(t => t.Id == productId);

            if (toode == null)
            {
                return NotFound("Toode not found.");
            }

            _context.KasutajaTooded.RemoveRange(toode.KasutajaTooted);
            _context.Tooted.Remove(toode);
            await _context.SaveChangesAsync();

            return Ok("Toode deleted successfully.");
        }
    }
}
