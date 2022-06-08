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
    public class EmpleadosController : Controller
    {
        private readonly ILogger<EmpleadosController> _logger;
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




        public EmpleadosController(ILogger<EmpleadosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            var Empleados = conexionBD.Empleado.ToList();

            return View(Empleados);
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
        public IActionResult Create(Empleado empleado)
        {
            GestionRRHHContext db = new GestionRRHHContext();
            db.Empleado.Add(empleado);
            db.SaveChanges();
            return View(empleado);
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

            var empleado = conexionBD.Empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Edit(int id, Empleado empleado)
        {

            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (id != empleado.CodigoEmpleado)
            {
                return NotFound();
            }

            if (elModeloEstaCorrecto)
            {
                try
                {
                    conexionBD.Update(empleado);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
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