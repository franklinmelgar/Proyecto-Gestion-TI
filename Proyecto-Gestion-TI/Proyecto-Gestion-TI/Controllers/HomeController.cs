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

        public IActionResult Index()
        {            
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            string nombre = HttpContext.Session.GetString("nombre");
            string tipo = HttpContext.Session.GetString("tipo");

            InformacionGeneral info = new InformacionGeneral()
            {
                NombreGeneralEmpleado = nombre,
                TipoGeneralEmpleado = tipo

            };            
            
            ViewBag.Nombre = nombre;

            return View(info);
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