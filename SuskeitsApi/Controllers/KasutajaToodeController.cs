using Microsoft.AspNetCore.Mvc;
using SuskeitsApi.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuskeitsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KasutajaToodeController : ControllerBase
    {
        private readonly SuskeitsDbContext _context;

        public KasutajaToodeController(SuskeitsDbContext context)
        {
            _context = context;
        }

        // GET https://localhost:7198/KasutajaToode/get-products/{userId}
        [HttpGet("get-products/{userId}")]
        public async Task<IActionResult> GetProductsForUser(int userId)
        {
            var kasutaja = await _context.Kasutajad
                .Include(k => k.KasutajaTooted) // Include the user's products through the join entity
                    .ThenInclude(kt => kt.Toode)
                .FirstOrDefaultAsync(k => k.Id == userId);

            if (kasutaja == null)
            {
                return NotFound("Kasutaja not found.");
            }

            var products = kasutaja.KasutajaTooted.Select(kt => kt.Toode).ToList();
            return Ok(products);
        }

        // POST https://localhost:7198/KasutajaToode/add-product
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToUser(int userId, string name, double price, bool isActive)
        {
            var kasutaja = await _context.Kasutajad
                .Include(k => k.KasutajaTooted)
                .FirstOrDefaultAsync(k => k.Id == userId);

            if (kasutaja == null)
            {
                return NotFound("Kasutaja not found.");
            }

            var toode = new Toode
            {
                Name = name,
                Price = price,
                IsActive = isActive
            };

            _context.Tooted.Add(toode);
            await _context.SaveChangesAsync();

            var kasutajaToode = new KasutajaToode
            {
                KasutajaId = userId,
                ToodeId = toode.Id
            };

            _context.KasutajaTooded.Add(kasutajaToode);
            await _context.SaveChangesAsync();

            return Ok(toode);
        }

        // DELETE https://localhost:7198/KasutajaToode/delete-product/{productId}
        [HttpDelete("delete-product/{productId}")]
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
