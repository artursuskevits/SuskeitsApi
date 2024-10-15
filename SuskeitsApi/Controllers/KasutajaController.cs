using SuskeitsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuskeitsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KasutajaController : ControllerBase
    {
        private static Kasutaja _kasutaja = new Kasutaja(1, "KoolaLover", "123ABC", "Vladlen","Semenov");

        // GET: toode
        [HttpGet]
        public Kasutaja GetKasutaja()
        {
            return _kasutaja;
        }

    }
}
