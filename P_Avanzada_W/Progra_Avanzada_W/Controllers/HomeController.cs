using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;
using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController(IUsuarioModel _usuarioModel, IUtilitariosModel _utilitariosModel, IProductoModel _productoModel) : Controller
    {

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.IniciarSesion(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("IdUsuario", resp?.Dato?.IdUsuario.ToString()!);
                HttpContext.Session.SetString("Correo", resp?.Dato?.Correo!);
                HttpContext.Session.SetString("Nombre", resp?.Dato?.NombreUsuario);
                HttpContext.Session.SetString("Token", resp?.Dato?.Token!);
                HttpContext.Session.SetString("IdRol", resp?.Dato?.IdRol.ToString()!);
                HttpContext.Session.SetString("IdUsuario", resp?.Dato?.IdUsuario.ToString()!);
                HttpContext.Session.SetString("NombreRol", resp?.Dato?.NombreRol!);


                HttpContext.Session.SetString("Cantidad", "");
                HttpContext.Session.SetString("Total", "");
                if ((bool)(resp?.Dato?.EsTemporal!))
                {
                    return RedirectToAction("CambiarContrasenna", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("Login", "true");
                    return RedirectToAction("PantallaPrincipal", "Home");
                }
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.RegistrarUsuario(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("IniciarSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario entidad)
        {
            var resp = _usuarioModel.RecuperarAcceso(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("IniciarSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult CambiarContrasenna()
        {
            var entidad = new Usuario();
            entidad.Correo = HttpContext.Session.GetString("Correo");
            return View(entidad);
        }

        [HttpPost]
        public IActionResult CambiarContrasenna(Usuario entidad)
        {
            if (entidad.Contrasenna?.Trim() == entidad.ContrasennaTemporal?.Trim())
            {
                ViewBag.MsjPantalla = "Debe utilizar una contraseña distinta";
                return View();
            }

            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            entidad.ContrasennaTemporal = _utilitariosModel.Encrypt(entidad.ContrasennaTemporal!);
            var resp = _usuarioModel.CambiarContrasenna(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("Login", "true");
                return RedirectToAction("PantallaPrincipal", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [Seguridad]
        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            var resp = _productoModel.ConsultarProductos();

            if (resp?.Codigo == "00")
                return View(resp?.Datos);
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View(new List<Producto>());
            }
        }


        [Seguridad]
        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("IniciarSesion", "Home");
        }

    }
}