using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;
using System.Diagnostics;

namespace Progra_Avanzada_W.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        public HomeController(UsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }

        [Seguridad]
        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            var resp = _usuarioModel.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction();

            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            return View();
        }
    }
}
