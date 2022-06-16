using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class VacacionesController : Controller
    {
        private readonly ILogger<Vacacione> _logger;
        private GestionRRHHContext conexionBD = new GestionRRHHContext();
        private string nombreEmpleado;
        private string tipoEmpleado;

        private bool EstaLaSesionActiva()
        {
            if (HttpContext.Session.GetString("codigo") == null)
            {
                return false;
            }

            nombreEmpleado = HttpContext.Session.GetString("nombre");
            tipoEmpleado = HttpContext.Session.GetString("tipo");

            ViewBag.Nombre = nombreEmpleado;
            ViewBag.Tipo = tipoEmpleado;

            return true;
        }


        public VacacionesController(ILogger<Vacacione> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            var vacaciones = conexionBD.Vacaciones.ToList();

            return View(vacaciones);
        }

        public IActionResult Create()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vacacione vacacion)
        {
            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (elModeloEstaCorrecto)
            {
                GestionRRHHContext db = new GestionRRHHContext();
                db.Vacaciones.Add(vacacion);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {

            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }


            if (id == null)
            {
                return NotFound();
            }

            var vacacion = conexionBD.Vacaciones.Find(id);
            if (vacacion == null)
            {
                return NotFound();
            }

            return View(vacacion);
        }

        [HttpPost]
        public IActionResult Edit(int id, Vacacione vacacion)
        {

            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (id != vacacion.CodigoVacacion)
            {
                return NotFound();
            }

            if (elModeloEstaCorrecto)
            {
                try
                {
                    conexionBD.Update(vacacion);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vacacion);
        }


        public IActionResult Delete(int? Id)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var vacacion = conexionBD.Vacaciones.Find(Id);

            return View(vacacion);
        }
        [HttpPost]
        public IActionResult Delete(int? Id, Vacacione vacacion)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }


            if (vacacion == null)
            {
                return NotFound();
            }
            else
            {
                conexionBD.Remove(vacacion);
                conexionBD.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
