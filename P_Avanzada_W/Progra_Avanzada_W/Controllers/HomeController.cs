using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Models;
using System.Diagnostics;

namespace Progra_Avanzada_W.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

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
        public IActionResult Registrarse()
        {
            return View();
        }
   
    }
}
