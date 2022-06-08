using Microsoft.AspNetCore.Mvc;
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
            
            GestionRRHHContext db = new GestionRRHHContext();
            var obj = db.Empleado.Where(a => a.Correo.Equals(usu.Correo) && a.Contrasena.Equals(usu.Contrasena)).FirstOrDefault();
            if (obj != null)
            {
                HttpContext.Session.SetString("codigo", obj.CodigoEmpleado.ToString());
                HttpContext.Session.SetString("nombre", obj.NombreEmpleado);
                HttpContext.Session.SetString("tipo", obj.CodigoTipoUsuario.ToString());
                return RedirectToAction("Index", "Home");
            }

            @ViewBag.Mensaje = "Usuario o Contrasena Incorrectos";
            return View(usu);
        }
    }
}
