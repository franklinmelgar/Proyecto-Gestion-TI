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
    public class TipoUsuariosController : Controller
    {
        private readonly ILogger<TipoUsuariosController> _logger;
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


        public TipoUsuariosController(ILogger<TipoUsuariosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            var tiposDeUsuario = conexionBD.TipoUsuarios.ToList();

            return View(tiposDeUsuario);
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
        public IActionResult Create(TipoUsuario tipoDeUsuario)
        {
            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (elModeloEstaCorrecto)
            {
                GestionRRHHContext db = new GestionRRHHContext();
                db.TipoUsuarios.Add(tipoDeUsuario);
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

            var tipoDeUsuario = conexionBD.TipoUsuarios.Find(id);
            if (tipoDeUsuario == null)
            {
                return NotFound();
            }

            return View(tipoDeUsuario);
        }

        [HttpPost]
        public IActionResult Edit(int id, TipoUsuario tipoDeUsuario)
        {

            bool elModeloEstaCorrecto = ModelState.IsValid;
            if (id != tipoDeUsuario.CodigoTipoUsuario)
            {
                return NotFound();
            }

            if (elModeloEstaCorrecto)
            {
                try
                {
                    conexionBD.Update(tipoDeUsuario);
                    conexionBD.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(tipoDeUsuario);
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

            var tipoDeUsuario = conexionBD.TipoUsuarios.Find(Id);

            return View(tipoDeUsuario);
        }
        [HttpPost]
        public IActionResult Delete(int? Id, TipoUsuario tipoDeUsuario)
        {
            if (!EstaLaSesionActiva())
            {
                return RedirectToAction("Index", "Login");
            }

            if (Id == null)
            {
                return NotFound();
            }


            if (tipoDeUsuario == null)
            {
                return NotFound();
            }
            else
            {
                conexionBD.Remove(tipoDeUsuario);
                conexionBD.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
