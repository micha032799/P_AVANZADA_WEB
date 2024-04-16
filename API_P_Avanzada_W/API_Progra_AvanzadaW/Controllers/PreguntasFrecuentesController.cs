using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using API_Progra_AvanzadaW.Entidades;
using Dapper;

namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasFrecuentesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public PreguntasFrecuentesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");

        }
        [HttpPost]
        [Route("RegistrarPreguntaFrecuente")]
        public IActionResult RegistrarPreguntaFrecuente(PreguntasFrecuentes entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {

                    var datos = context.Execute("RegistrarPreguntaFrecuente",
                        new
                        {
                            entidad.IdUsuario,
                            entidad.Pregunta,
                            entidad.Respuesta
                        },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        [Route("ActualizarPreguntasFrecuentes")]
        public IActionResult ActualizarPreguntasFrecuentes(PreguntasFrecuentes entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarPreguntasFrecuentes",
                        new
                        {
                            entidad.IdPreguntasFrecuentes,
                            entidad.Pregunta,
                            entidad.Respuesta

                        },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("ObtenerPreguntaFrecuentePorId")]
        public IActionResult ObtenerPreguntaFrecuentePorId(long q)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<PreguntasFrecuentes>("ObtenerPreguntaFrecuentePorId",
                     new { IdPreguntasFrecuentes = q },
                     commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarPreguntaFrecuentePorId")]
        public IActionResult EliminarPreguntaFrecuentePorId(long q)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarPreguntaFrecuentePorId",
                       new { IdPreguntasFrecuentes = q },
                       commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet]
        [Route("ObtenerTodasLasPreguntasFrecuentes")]
        public IActionResult ObtenerTodasLasPreguntasFrecuentes()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<PreguntasFrecuentes>("ObtenerTodasLasPreguntasFrecuentes",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
