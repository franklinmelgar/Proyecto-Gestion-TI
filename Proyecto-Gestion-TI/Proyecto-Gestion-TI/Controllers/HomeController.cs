using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;
using System.Diagnostics;

namespace Proyecto_Gestion_TI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private GestionRRHHContext conexionBD = new GestionRRHHContext();
        private enum Estado
        {
            Abierto = 1,
            Cerrado = 2
        }

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
            string codigo = HttpContext.Session.GetString("codigo");

            ViewBag.Nombre = nombre;
            ViewBag.Tipo = tipo;


            ViewBag.Fecha = DateTime.Today.ToShortDateString();

            var listadoConsultas = new List<Consulta>();

            if (tipo == "RRHH")
            {
                listadoConsultas = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(a => a.EstadoConsulta.Equals((int)Estado.Abierto)).ToList();
            }
            else
            {
                listadoConsultas = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)) && a.EstadoConsulta.Equals((int)Estado.Abierto)).ToList();
            }

            ViewData["Consultas"] = listadoConsultas;


            var listadoSolicitudes = new List<SolicitudVacacione>();

            if (tipo == "RRHH")
            {
                listadoSolicitudes = conexionBD.SolicitudVacaciones.ToList();
            }
            else
            {
                listadoSolicitudes = conexionBD.SolicitudVacaciones.Where(S => S.CodigoEmpleado.Equals(Convert.ToInt32(codigo))).ToList();
            }

            ViewData["Solicitudes"] = listadoSolicitudes;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}