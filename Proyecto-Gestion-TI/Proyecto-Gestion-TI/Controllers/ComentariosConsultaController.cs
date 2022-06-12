using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class ComentariosConsultaController : Controller
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
            codigo = HttpContext.Session.GetString("codigo");

            ViewBag.Nombre = nombre;
            ViewBag.Tipo = tipo;

            return true;
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

        [HttpPost]
        public IActionResult Detail(ComentariosConsulta comen)
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            if (comen == null)
            {
                return RedirectToAction("Index", "Consulta");
            }

            comen.FechaComentario = DateTime.Today;

            int Id = (int)comen.CodigoConsulta;

            conexionBD.ComentariosConsulta.Add(comen);
            conexionBD.SaveChanges();

            var comentarios = conexionBD.Set<ComentariosConsulta>().Include(CC => CC.CodigoConsultaNavigation).Include(CC => CC.CodigoEmpleadoComentarioNavigation).Where(CC => CC.CodigoConsulta.Equals(Id)).ToList();
            var consulta = conexionBD.Set<Consulta>().Include(C => C.CodigoEmpleadoNavigation).Where(C => C.CodigoConsulta.Equals(Id)).FirstOrDefault();
            ViewData["Consulta"] = consulta;
            ViewData["Comentarios"] = comentarios;

            return View();
        }

    }
}
