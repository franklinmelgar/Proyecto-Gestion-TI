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

            var listadoMisConsultas = conexionBD.Consulta.Where(a => a.CodigoEmpleado.Equals(int.Parse(codigo)));

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

            var consulta = conexionBD.Set<Consulta>().Include(E => E.CodigoEmpleadoNavigation).Include(C => C.ComentariosConsulta).Where(C => C.CodigoConsulta.Equals(Id)).FirstOrDefault();

            if (consulta == null)
            {
                return NotFound();
            }

            var comentariosLista = new List<ComentariosConsulta>();
            foreach (ComentariosConsulta comentario in consulta.ComentariosConsulta)
            {
                comentariosLista.Add(comentario);
            }

            ViewData["Comentarios"] = comentariosLista;

            return View(consulta);
        }


    }
}
