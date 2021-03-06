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
        private enum Estado
        {
            Abierto = 1,
            Cerrado = 2
        }

        private bool ValidarInicioSesion()
        {
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return false;
            }

            nombre = HttpContext.Session.GetString("nombre");
            tipo = HttpContext.Session.GetString("tipo");
            codigo = HttpContext.Session.GetString("codigo");

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

            ViewBag.Tipo = tipo;

            var listadoMisConsultas = new List<Consulta>();


            if (tipo == "RRHH")
            {
                listadoMisConsultas = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(a => a.EstadoConsulta.Equals((int)Estado.Abierto)).ToList();
            }
            else
            {
                listadoMisConsultas = conexionBD.Consulta.Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)) && a.EstadoConsulta.Equals((int)Estado.Abierto)).ToList();
            }            

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
