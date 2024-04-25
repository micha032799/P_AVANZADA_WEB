using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;
using System.Data;

namespace Progra_Avanzada_W.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class ReservaController(IReservaModel _reservaModel, IUsuarioModel _usuarios) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarReservas()
        {
            var resp = _reservaModel.ConsultarReservas();

            if (resp?.Codigo == "00")
                return View(resp?.Datos);
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View(new List<Reserva>());
            }
        }

        [HttpGet]
        public IActionResult ObtenerReservas()
        {
            var resp = _reservaModel.ConsultarReservas();

            if (resp?.Codigo == "00")
                return Json(resp?.Datos);
            else
                return Json(new List<Reserva>());
        }

        [HttpGet]
        public IActionResult AgregarReserva()
        {
            CargarClientes();
            return View();
        }

        [HttpPost]
        public IActionResult AgregarReserva(Reserva entidad)
        {
            entidad.Precio = (entidad.FechaReserva.DayOfWeek == DayOfWeek.Saturday || entidad.FechaReserva.DayOfWeek == DayOfWeek.Sunday ? 8000 : 7500);
            var resp = _reservaModel.RegistrarReserva(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarReservas", "Reserva");
            else
            {
                CargarClientes();
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpGet]
        public IActionResult ActualizarReserva(long id)
        {
            var resp = _reservaModel.ConsultarReserva(id);

            ViewBag.FechaReservaHide = resp?.Dato?.FechaFinReserva.ToString("yyyy-MM-dd HH:mm");
            CargarClientes();
            return View(resp?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarReserva(Reserva entidad)
        {
            entidad.Precio = (entidad.FechaReserva.DayOfWeek == DayOfWeek.Saturday || entidad.FechaReserva.DayOfWeek == DayOfWeek.Sunday ? 8000 : 7500);
            var resp = _reservaModel.ActualizarReserva(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarReservas", "Reserva");
            else
            {
                ViewBag.FechaReservaHide = entidad.FechaFinReserva.ToString("yyyy-MM-dd HH:mm");
                CargarClientes();
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarReserva(Reserva entidad)
        {
            var resp = _reservaModel.EliminarReserva(entidad.IdReserva);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarReservas", "Reserva");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }
        private void CargarClientes()
        {
            var lista = new List<SelectListItem>();
            var usuarios = _usuarios.ConsultarUsuario(1)?.Datos;

            if (usuarios != null)
            {
                foreach (var item in usuarios.Where(x => x.IdRol == 1))
                {
                    lista.Add(new SelectListItem { Value = item.IdUsuario.ToString(), Text = item.NombreUsuario });
                }
            }
            lista.Insert(0, new SelectListItem { Value = string.Empty, Text = "Usuario" });
            ViewBag.Clientes = lista;
        }
    }
}
