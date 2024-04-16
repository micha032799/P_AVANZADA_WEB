using Microsoft.AspNetCore.Mvc;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Models;
using Progra_Avanzada_W.Services;

namespace Progra_Avanzada_W.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class PreguntasFrecuentesController : Controller
    {
        private readonly IPreguntasFrecuentesModel _PreguntasFrecuentesModel;
        public PreguntasFrecuentesController(IPreguntasFrecuentesModel PreguntasFrecuentesModel)
        {
            _PreguntasFrecuentesModel = PreguntasFrecuentesModel;
        }

        [HttpGet]
        public IActionResult RegistrarPreguntaFrecuente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarPreguntaFrecuente(PreguntasFrecuentes entidad)
        {
          
            string idUsuarioString = HttpContext.Session.GetString("IdUsuario");
            if (long.TryParse(idUsuarioString, out long idUsuario))
            {
                entidad.IdUsuario = idUsuario;
            }
            var resp = _PreguntasFrecuentesModel.RegistrarPreguntaFrecuente(entidad);

            if (resp == 1)
                return RedirectToAction("ObtenerTodasLasPreguntasFrecuentes", "PreguntasFrecuentes");
            else
            {
                ViewBag.MensajePantalla = "No se logró registrar la pregunta frecuente";
                return View();
            }
        }


        [HttpGet]
        public IActionResult ObtenObtenerPreguntaFrecuentePorIderConsultaPorId()
        {
            return View();
        }



        [HttpGet]
        public IActionResult ObtenerPreguntaFrecuentePorId(long q)
        {
            var datos = _PreguntasFrecuentesModel.ObtenerPreguntaFrecuentePorId(q);
            return View(datos);
        }


        [HttpGet]
        public IActionResult ActualizarPreguntasFrecuentes(long q)
        {
            var datos = _PreguntasFrecuentesModel.ObtenerTodasLasPreguntasFrecuentes().Where(x => x.IdPreguntasFrecuentes == q).FirstOrDefault();
            return View(datos);
        }




        [HttpPost]
        public IActionResult ActualizarPreguntasFrecuentes(PreguntasFrecuentes entidad)
        {
            var resp = _PreguntasFrecuentesModel.ActualizarPreguntasFrecuentes(entidad);

            if (resp == 1)
            {
                return RedirectToAction("ObtenerTodasLasPreguntasFrecuentes", "PreguntasFrecuentes");
            }
            else
            {
                ViewBag.MensajePantalla = "No se logró actualizar la pregunta frecuente";
                return View();
            }
        }



        [HttpGet]
        public IActionResult ObtenerTodasLasPreguntasFrecuentes()
        {

            var datos = _PreguntasFrecuentesModel.ObtenerTodasLasPreguntasFrecuentes();
            return View(datos);

        }

        [HttpGet]
        public IActionResult EliminarPreguntaFrecuentePorId(long q)
        {
            _PreguntasFrecuentesModel.EliminarPreguntaFrecuentePorId(q);

            return RedirectToAction("ObtenerTodasLasPreguntasFrecuentes", "PreguntasFrecuentes");
        }
    }
}

