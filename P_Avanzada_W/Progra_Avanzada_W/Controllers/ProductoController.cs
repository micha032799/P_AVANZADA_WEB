using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;
using System.Collections.Generic;
using System.Linq;

namespace Progra_Avanzada_W.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class ProductoController : Controller
    {
        private readonly IProductoModel _productoModel;

        public ProductoController(IProductoModel productoModel)
        {
            _productoModel = productoModel;
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
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

        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto entidad)
        {
            var resp = _productoModel.RegistrarProducto(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarProductoPorId(Producto entidad)
        {
            var resp = _productoModel.EliminarProductoPorId(entidad.IdProducto);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return RedirectToAction("ConsultarProductos", "Producto");
            }
        }

        [HttpGet]
        public IActionResult EditarProducto(long q)
        {
            var resp = _productoModel.ConsultarProductos();
            if (resp?.Codigo == "00")
            {
                var datos = resp.Datos.Where(x => x.IdProducto == q).FirstOrDefault();
                return View(datos);
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }
        [HttpPost]
        public IActionResult EditarProducto(Producto entidad)
        {
            var resp = _productoModel.EditarProducto(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return RedirectToAction("ConsultarProductos", "Producto");
            }
        }
    }
}
