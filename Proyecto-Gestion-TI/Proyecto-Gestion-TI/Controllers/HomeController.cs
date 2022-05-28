using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_TI.Models;
using System.Diagnostics;

namespace Proyecto_Gestion_TI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult establerNombre()
        {
            string nombre = HttpContext.Session.GetString("nombre");
            ViewBag.Nombre = nombre;

            return PartialView("_SetNombre");
        }

        public IActionResult Index()
        {            
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}