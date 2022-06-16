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
    public class DepartamentoController : Controller
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

        
        public DepartamentoController(ILogger<EmpleadosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            var departamentos = conexionBD.Departamentos.ToList();

            return View(departamentos);
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
        public IActionResult Create(Departamento departamento)
        {
            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (elModeloEstaCorrecto)
            {
                GestionRRHHContext db = new GestionRRHHContext();
                db.Departamentos.Add(departamento);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
            //return View(departamento);
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

            var departamento = conexionBD.Departamentos.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        [HttpPost]
        public IActionResult Edit(int id, Departamento departamento)
        {

            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (id != departamento.CodigoDepartamento)
            {
                return NotFound();
            }

            if (elModeloEstaCorrecto)
            {
                try
                {
                    conexionBD.Update(departamento);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
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

            var departamento = conexionBD.Departamentos.Find(Id);

            return View(departamento);
        }
        [HttpPost]
        public IActionResult Delete(int? Id, Departamento departamento)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }

  
            if (departamento == null)
            {
                return NotFound();
            }
            else
            {
                conexionBD.Remove(departamento);
                conexionBD.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
