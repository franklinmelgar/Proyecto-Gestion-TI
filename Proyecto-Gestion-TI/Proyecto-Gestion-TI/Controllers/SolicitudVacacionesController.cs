using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class SolicitudVacacionesController : Controller
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

        public IActionResult Index()
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Tipo = tipo;

            var listadoSolicitudes = conexionBD.SolicitudVacaciones.Where(S => S.CodigoEmpleado.Equals(int.Parse(codigo))).ToList();            

            return View(listadoSolicitudes);
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
        public IActionResult Create(SolicitudVacacione solicitud)
        {

            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            GestionRRHHContext db = new GestionRRHHContext();
            solicitud.CodigoEmpleado = int.Parse(codigo);

            db.SolicitudVacaciones.Add(solicitud);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? Id)
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            var solicitud = conexionBD.Set<SolicitudVacacione>().Include(S => S.CodigoEmpleadoNavigation).Where(S => S.CodigoSolicitud.Equals(Id)).FirstOrDefault();
            int codigoDepartamento = (int)solicitud.CodigoEmpleadoNavigation.CodigoDepartamento;

            var aprobadores = conexionBD.Set<RutaAprobacion>().Include(R => R.CodigoAprobadorNavigation).Where(R => R.CodigoDepartamento.Equals(codigoDepartamento)).OrderBy(R => R.NivelAprobacion).ToList();

            ViewData["Solicitud"] = solicitud;
            ViewData["Aprobadores"] = aprobadores;


            return View();
        }
    }
}
