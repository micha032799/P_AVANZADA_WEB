using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;

namespace Progra_Avanzada_W.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;
        private readonly IBitacoraModel _bitacoraModel;

        private readonly IProductoModel _productoModel;
        private readonly IUsuarioModel _usuarioModel;

        public CarritoController(ICarritoModel carritoModel, IBitacoraModel bitacoraModel, IProductoModel productoModel, IUsuarioModel usuarioModel)
        {
            _carritoModel = carritoModel;
            _bitacoraModel = bitacoraModel;
            _productoModel = productoModel;
            _usuarioModel = usuarioModel;
        }

        [HttpPost]
        public IActionResult RegistrarCarrito(long IdProducto, int cantidadComprar)
        {
            var entidad = new CarritoEnt();
            entidad.Cantidad = cantidadComprar;
            entidad.IdProducto = IdProducto;

            _carritoModel.RegistrarCarrito(entidad);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            return Json("OK");
        }

        [HttpGet]
        public IActionResult ConsultarCarrito()
        {
            var datos = _carritoModel.ConsultarCarrito();

            if (datos == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                foreach (var item in datos)
                {
                    var usuario = _usuarioModel.ConsultarUsuario(item.IdUsuario);
                    var producto = _productoModel.ObtenerInventarioPorId(item.IdProducto);

                    item.nomProducto = (producto != null) ? producto.Dato.nombre : "";
                    item.nomUsuario = (usuario != null) ? usuario.Dato.NombreUsuario : "";

                }
                datos = datos.OrderBy(s => s.Fecha).ToList();
                return View(datos);
            }
        }

        [HttpPost]
        public IActionResult PagarCarrito()
        {
            try
            {
                var respuesta = _carritoModel.PagarCarrito();
                var datos = _carritoModel.ConsultarCarrito();
                HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
                HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

                if (respuesta.Mensaje.Contains("verifique"))
                {
                    ViewBag.MensajePantalla = respuesta;
                    return View("ConsultarCarrito", datos);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                var entidad = new BitacoraEnt();
                entidad.FechaError = DateTime.Now;
                entidad.Error = ex.Message;

                _bitacoraModel.RegistrarErrorBitacora(entidad);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public IActionResult EliminarProductoCarrito(long q)
        {
            _carritoModel.EliminarProductoCarrito(q);

            var datos = _carritoModel.ConsultarCarrito();
            HttpContext.Session.SetString("Total", datos.Sum(x => x.Total).ToString());
            HttpContext.Session.SetString("Cantidad", datos.Sum(x => x.Cantidad).ToString());

            return RedirectToAction("ConsultarCarrito", "Carrito");
        }

        [HttpGet]
        public IActionResult ConsultarFacturas()
        {
            var datos = _carritoModel.ConsultarFacturas();
            return View(datos);
        }

        [HttpGet]
        public IActionResult ConsultarDetalleFactura(long q)
        {
            var datos = _carritoModel.ConsultarDetalleFactura(q);
            return View(datos);
        }
    }
}
