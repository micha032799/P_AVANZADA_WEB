using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;
using Progra_Avanzada_W.Entidades;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Progra_Avanzada_W.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class UsuarioController(IUsuarioModel _usuarioModel, IUtilitariosModel _utilitariosModel) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarPerfil()
        {
            var resp = _usuarioModel.ConsultarUsuario();
            return View(resp?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.ActualizarPerfil(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("Correo", entidad.Correo!);
                HttpContext.Session.SetString("Nombre", entidad.NombreUsuario!);
                return RedirectToAction("ConsultarPerfil", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }
    }
}