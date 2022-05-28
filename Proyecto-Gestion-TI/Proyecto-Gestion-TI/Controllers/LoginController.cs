using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_TI.Models;

namespace Proyecto_Gestion_TI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usu)
        {
            GestionRRHHContext db = new GestionRRHHContext();
            var obj = db.Usuarios.Where(a => a.CorreoElectronico.Equals(usu.CorreoElectronico) && a.ContrasenaUsuario.Equals(usu.ContrasenaUsuario)).FirstOrDefault();
            if (obj != null)
            {
                HttpContext.Session.SetString("codigo", obj.CodigoEmpleado.ToString());
                HttpContext.Session.SetString("nombre", obj.NombreUsuario);

                return RedirectToAction("Index", "Home");
            }

            return View(usu);
        }
    }
}
