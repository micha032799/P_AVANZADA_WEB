using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;

namespace Progra_Avanzada_W.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class ProductoController(IProductoModel _productoModel) : Controller
    {
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
    }
}
