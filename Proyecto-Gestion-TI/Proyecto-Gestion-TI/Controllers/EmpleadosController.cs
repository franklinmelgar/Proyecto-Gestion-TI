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

            ListasDesplegables listas = new ListasDesplegables();

            ViewBag.puesto = listas.GenerarlistaDesplegableDePuestos();
            ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            ViewBag.jefesDeEmpleados = listas.GenerarlistaDesplegableDeJefes();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Empleado empleado)
        {
            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (elModeloEstaCorrecto) 
            {
            }
            GestionRRHHContext db = new GestionRRHHContext();
            db.Add(empleado);
            db.SaveChanges();

            ListasDesplegables listas = new ListasDesplegables();

            ViewBag.puesto = listas.GenerarlistaDesplegableDePuestos();
            ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            ViewBag.jefesDeEmpleados = listas.GenerarlistaDesplegableDeJefes();
         
            return View(empleado);
        }

        public IActionResult Edit(int? id)
        {
            
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

          
            var empleado = conexionBD.Empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            ListasDesplegables listas = new ListasDesplegables();

            ViewBag.puesto = listas.GenerarlistaDesplegableDePuestos();
            ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            ViewBag.jefesDeEmpleados = listas.GenerarlistaDesplegableDeJefes();
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
            var empleado = conexionBD.Empleado.Find(Id);
            return View(empleado);
        }

        [HttpPost]
        public IActionResult Delete(int? Id,Empleado empleado)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }


            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                conexionBD.Remove(empleado);
                conexionBD.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}