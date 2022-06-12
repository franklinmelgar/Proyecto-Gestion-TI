using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ILogger<ConsultaController> _logger;
        private GestionRRHHContext conexionBD = new GestionRRHHContext();
        private string nombre;
        private string tipo;
        private string codigo;

        private bool ValidarInicioSesion()
        {
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return false;
            }

            nombre = HttpContext.Session.GetString("nombre");
            tipo = HttpContext.Session.GetString("tipo");
            codigo = HttpContext.Session.GetString("tipo");

            ViewBag.Nombre = nombre;
            ViewBag.Tipo = tipo;

            return true;
        }

        public ConsultaController(ILogger<ConsultaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            var listadoMisConsultas = conexionBD.Consulta.Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)) && a.EstadoConsulta.Equals(1));

            return View(listadoMisConsultas);
        }


        public IActionResult Create()
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Consulta consulta)
        {

            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            GestionRRHHContext db = new GestionRRHHContext();
            consulta.FechaConsulta = DateTime.Today;
            consulta.CodigoEmpleado = int.Parse(codigo);
            consulta.EstadoConsulta = 1;
            db.Consulta.Add(consulta);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
