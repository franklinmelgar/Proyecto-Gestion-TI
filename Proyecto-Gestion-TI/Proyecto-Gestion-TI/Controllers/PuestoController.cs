using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;
using System.Diagnostics;

namespace Proyecto_Gestion_TI.Controllers
{
    public class PuestoController : Controller
    {
        private readonly ILogger<PuestoController> _logger;
        private GestionRRHHContext conexionBD = new GestionRRHHContext();
        private string nombre;
        private string tipo;

        private bool ValidarInicioSesion()
        {
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return false;
            }

            nombre = HttpContext.Session.GetString("nombre");
            tipo = HttpContext.Session.GetString("tipo");

            ViewBag.Nombre = nombre;
            ViewBag.Tipo = tipo;

            return true;
        }

        public PuestoController(ILogger<PuestoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            var listadoPuestos = conexionBD.Puestos.ToList();

            return View(listadoPuestos);
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
        public IActionResult Create(Puesto puesto)
        {
            GestionRRHHContext db = new GestionRRHHContext();
            db.Puestos.Add(puesto);
            db.SaveChanges();
            return View(puesto);
        }

        public IActionResult Edit(int? Id)
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var puesto = conexionBD.Puestos.Find(Id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        [HttpPost]
        public IActionResult Edit(int id, Puesto puesto)
        {
            if (id != puesto.CodigoPuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    conexionBD.Update(puesto);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(puesto);
        }

        public IActionResult Delete(int? Id)
        {
            if (!ValidarInicioSesion())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var puesto = conexionBD.Puestos.Find(Id);
            if (puesto == null)
            {
                return NotFound();
			}
			else
			{
                conexionBD.Remove(puesto);
                conexionBD.SaveChanges();
			}

            return View();
        }

    }

}
