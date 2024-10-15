using Microsoft.AspNetCore.Mvc;

namespace SuskeitsApi.Controllers
{
    public class KasutajadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
