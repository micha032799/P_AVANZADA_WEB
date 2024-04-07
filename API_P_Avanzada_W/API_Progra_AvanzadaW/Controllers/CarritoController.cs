using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Progra_AvanzadaW.Entities;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarritoController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("RegistrarCarrito")]
        public IActionResult RegistrarCarrito(CarritoEnt entidad)
        {
            try
            {
                long IdUsuariol = 0;
                string idUsuarioString = _httpContextAccessor.HttpContext.Session.GetString("IdUsuario");
                if (long.TryParse(idUsuarioString, out long idUsuario))
                {
                    IdUsuariol = idUsuario;
                }
                long IdUsuario = IdUsuariol;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("RegistrarCarrito",
                        new { IdUsuario, entidad.IdProducto, entidad.Cantidad },
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
        [Route("ConsultarCarrito")]
        public IActionResult ConsultarCarrito()
        {
            try
            {
                long IdUsuariol = 0;
                string idUsuarioString = _httpContextAccessor.HttpContext.Session.GetString("IdUsuario");
                if (long.TryParse(idUsuarioString, out long idUsuario))
                {
                    IdUsuariol = idUsuario;
                }
                long IdUsuario = IdUsuariol;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<CarritoEnt>("ConsultarCarrito",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PagarCarrito")]
        public IActionResult PagarCarrito()
        {
            try
            {
                long IdUsuariol = 0;
                string idUsuarioString = _httpContextAccessor.HttpContext.Session.GetString("IdUsuario");
                if (long.TryParse(idUsuarioString, out long idUsuario))
                {
                    IdUsuariol = idUsuario;
                }
                long IdUsuario = IdUsuariol;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<string>("PagarCarrito",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
                    //_httpContextAccessor.HttpContext.Session.Clear();
                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarProductoCarrito")]
        public IActionResult EliminarProductoCarrito(long q)
        {
            try
            {
                long IdCarrito = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("EliminarProductoCarrito",
                        new { IdCarrito },
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
        [Route("ConsultarFacturas")]
        public IActionResult ConsultarFacturas()
        {
            try
            {
                long IdUsuariol = 0;
                string idUsuarioString = HttpContext.Session.GetString("IdUsuario");
                if (long.TryParse(idUsuarioString, out long idUsuario))
                {
                    IdUsuariol = idUsuario;
                }
                long IdUsuario = IdUsuariol;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<FacturasEnt>("ConsultarFacturas",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarDetalleFactura")]
        public IActionResult ConsultarDetalleFactura(long q)
        {
            try
            {
                long IdFactura = q;

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<FacturasEnt>("ConsultarDetalleFactura",
                        new { IdFactura },
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
