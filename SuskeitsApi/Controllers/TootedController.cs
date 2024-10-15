using Microsoft.AspNetCore.Mvc;
using SuskeitsApi.Models;

namespace SuskeitsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private static List<Toode> _tooted = new()
        {
            new Toode(1,"Koola", 1.5, true),
            new Toode(2,"Fanta", 1.0, false),
            new Toode(3,"Sprite", 1.7, true),
            new Toode(4,"Vichy", 2.0, true),
            new Toode(5,"Vitamin well", 2.5, true)
        };

        // GET https://localhost:7198/tooted
        [HttpGet]
        public List<Toode> Get()
        {
            return _tooted;
        }

        // DELETE https://localhost:7198/tooted/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Toode> Delete(int index)
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
        [HttpPost("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpPost("lisa2")]
        public List<Toode> Add2(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        // PATCH https://localhost:7198/tooted/hind-dollaritesse/1.5
        [HttpPatch("hind-dollaritesse/{kurss}")]
        public List<Toode> UpdatePrices(double kurss)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].Price = _tooted[i].Price * kurss;
            }
            return _tooted;
        }
    }
}