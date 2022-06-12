using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("codigo");

            return View();
        }

        [HttpPost]
        public IActionResult Index(Empleado usu)
        {
            
            GestionRRHHContext conexionDB = new GestionRRHHContext();
            //var obj = db.Empleado.Where(a => a.Correo.Equals(usu.Correo) && a.Contrasena.Equals(usu.Contrasena)).FirstOrDefault();
            var usuario = conexionDB.Set<Empleado>().Include(E => E.CodigoTipoUsuarioNavigation).Where(a => a.Correo.Equals(usu.Correo) && a.Contrasena.Equals(usu.Contrasena)).FirstOrDefault();

            if (usuario != null)
            {
                HttpContext.Session.SetString("codigo", usuario.CodigoEmpleado.ToString());
                HttpContext.Session.SetString("nombre", usuario.NombreEmpleado);
                HttpContext.Session.SetString("tipo", usuario.CodigoTipoUsuarioNavigation.NombreTipoUsurio);
                return RedirectToAction("Index", "Home");
            }

            @ViewBag.Mensaje = "Usuario o Contrasena Incorrectos";
            return View(usu);
        }
    }
}
