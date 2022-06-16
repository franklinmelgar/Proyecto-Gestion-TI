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
    public class RutaAprobacionController : Controller
    {
        private readonly ILogger<RutaAprobacionController> _logger;
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

        public RutaAprobacionController(ILogger<RutaAprobacionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            var rutasDeAprobacion = conexionBD.RutaAprobacions.ToList();

            return View(rutasDeAprobacion);
        }

        public IActionResult Create()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            ListasDesplegables listas = new ListasDesplegables();

            ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            return View();
        }

        [HttpPost]
        public IActionResult Create(RutaAprobacion rutaAprobacion)
        {
            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (elModeloEstaCorrecto)
            {
            }
            GestionRRHHContext db = new GestionRRHHContext();
            db.Add(rutaAprobacion);
            db.SaveChanges();

            ListasDesplegables listas = new ListasDesplegables();

            //ViewBag.puesto = listas.GenerarlistaDesplegableDePuestos();
            //ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            //ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            //ViewBag.jefesDeEmpleados = listas.GenerarlistaDesplegableDeJefes();

            return View(rutaAprobacion);
        }

        public IActionResult Edit(int? id)
        {

            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }


            var rutaAprobacion = conexionBD.RutaAprobacions.Find(id);
            if (rutaAprobacion == null)
            {
                return NotFound();
            }

            //ListasDesplegables listas = new ListasDesplegables();

            //ViewBag.puesto = listas.GenerarlistaDesplegableDePuestos();
            //ViewBag.departamento = listas.GenerarListaDesplegableDeDepartamentos();
            //ViewBag.tipoDeUsuario = listas.GenerarlistaDesplegableDeTiposDeUsuario();
            //ViewBag.jefesDeEmpleados = listas.GenerarlistaDesplegableDeJefes();
            return View(rutaAprobacion);
        }

        [HttpPost]
        public IActionResult Edit(int id, RutaAprobacion rutaDeAprobacion)
        {

            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (id != rutaDeAprobacion.CodigoRuta)
            {
                return NotFound();
            }

            if (elModeloEstaCorrecto)
            {
                try
                {
                    conexionBD.Update(rutaDeAprobacion);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(rutaDeAprobacion);
        }

        public IActionResult Delete(int? Id)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }
            var rutaDeAprobacion = conexionBD.RutaAprobacions.Find(Id);
            return View(rutaDeAprobacion);
        }

        [HttpPost]
        public IActionResult Delete(int? Id, RutaAprobacion rutaDeAprobacion)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }


            if (rutaDeAprobacion == null)
            {
                return NotFound();
            }
            else
            {
                conexionBD.Remove(rutaDeAprobacion);
                conexionBD.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
