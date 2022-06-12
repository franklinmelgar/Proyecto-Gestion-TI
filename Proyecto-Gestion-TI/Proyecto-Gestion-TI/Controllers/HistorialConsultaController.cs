using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class HistorialConsultaController : Controller
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

            var listadoMisConsultas = new List<Consulta>();


            if (tipo == "RRHH")
            {
                listadoMisConsultas = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(a => a.EstadoConsulta.Equals((int)Estado.Cerrado)).ToList();
            }
            else
            {
                listadoMisConsultas = conexionBD.Consulta.Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)) && a.EstadoConsulta.Equals((int)Estado.Cerrado)).ToList();
            }

            //var listadoMisConsultas = conexionBD.Consulta.Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)) && a.EstadoConsulta.Equals((int)Estado.Cerrado));

            return View(listadoMisConsultas);
        }

        public IActionResult Detail(int? Id)
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }


            if (Id == null)
            {
                return NotFound();
            }

            var comentarios = conexionBD.Set<ComentariosConsulta>().Include(CC => CC.CodigoConsultaNavigation).Include(CC => CC.CodigoEmpleadoComentarioNavigation).Where(CC => CC.CodigoConsulta.Equals(Id)).ToList();
            var consulta = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(C => C.CodigoConsulta.Equals(Id)).FirstOrDefault();
            ViewData["Consulta"] = consulta;
            ViewData["Comentarios"] = comentarios;


            return View();
        }
    }
}
