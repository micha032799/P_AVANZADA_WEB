using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API_Progra_AvanzadaW.Entidades;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using API_Progra_AvanzadaW.Services;

namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController(IUtilitariosModel _utilitariosModel, IConfiguration _configuration) : ControllerBase
    {
        [Authorize]
        [Route("RegistrarReserva")]
        [HttpPost]
        public IActionResult RegistrarReserva(Reserva entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();
                long IdUsuario = long.Parse(_utilitariosModel.Decrypt(User.Identity!.Name!));

                entidad.IdUsuario = (entidad.IdUsuario != 0 ? entidad.IdUsuario : IdUsuario);

                var result = db.Execute("RegistrarReserva",
                    new { entidad.IdUsuario, entidad.CantidadPersonas, entidad.FechaReserva, entidad.Precio },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Ya realizó una reserva para el día de hoy.";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ActualizarReserva")]
        [HttpPut]
        public IActionResult ActualizarReserva(Reserva entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();
                long IdUsuario = long.Parse(_utilitariosModel.Decrypt(User.Identity!.Name!));

                entidad.IdUsuario = (entidad.IdUsuario != 0 ? entidad.IdUsuario : IdUsuario);

                var result = db.Execute("ActualizarReserva",
                    new { entidad.IdReserva, entidad.IdUsuario, entidad.CantidadPersonas, entidad.FechaReserva, entidad.Precio },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Ya realizó una reserva para el día de hoy.";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("EliminarReserva")]
        [HttpDelete]
        public IActionResult EliminarReserva(long IdReserva)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();
                long IdUsuario = long.Parse(_utilitariosModel.Decrypt(User.Identity!.Name!));

                var result = db.Execute("EliminarReserva",
                    new { IdReserva },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No se ha podido eliminar la reserva.";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ConsultarReservas")]
        [HttpGet]
        public IActionResult ConsultarReservas()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();
                long IdUsuario = long.Parse(_utilitariosModel.Decrypt(User.Identity!.Name!));

                var result = db.Query<Reserva>("ConsultarReservas",
                    new { IdUsuario },
                    commandType: CommandType.StoredProcedure).ToList();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay reservas registrados.";
                }
                else
                {
                    respuesta.Datos = result;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ConsultarReserva")]
        [HttpGet]
        public IActionResult ConsultarReserva(long IdReserva)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ReservaRespuesta respuesta = new ReservaRespuesta();

                var result = db.Query<Reserva>("ConsultarReserva",
                    new { IdReserva },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay reservas registrados.";
                }
                else
                {
                    respuesta.Dato = result;
                }

                return Ok(respuesta);
            }
        }
    }
}
