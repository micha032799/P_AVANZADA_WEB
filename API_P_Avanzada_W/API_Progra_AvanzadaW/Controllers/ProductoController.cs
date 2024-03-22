using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API_Progra_AvanzadaW.Entidades;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(IConfiguration _configuration) : ControllerBase
    {
        [Authorize]
        [Route("ConsultarProductos")]
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ProductoRespuesta respuesta = new ProductoRespuesta();

                var result = db.Query<Producto>("ConsultarProductos",
                    new { },
                    commandType: CommandType.StoredProcedure).ToList();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay productos registrados.";
                }
                else
                {
                    respuesta.Datos = result;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ConsultarProducto")]
        [HttpGet]
        public IActionResult ConsultarProducto(long IdProducto)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ProductoRespuesta respuesta = new ProductoRespuesta();

                var result = db.Query<Producto>("ConsultarProducto",
                    new { IdProducto },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay productos registrados.";
                }
                else
                {
                    respuesta.Dato = result;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ConsultarCategorias")]
        [HttpGet]
        public IActionResult ConsultarCategorias()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                CategoriaRespuesta respuesta = new CategoriaRespuesta();

                var result = db.Query<Categoria>("ConsultarCategorias",
                    new { },
                    commandType: CommandType.StoredProcedure).ToList();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay categor�as registradas.";
                }
                else
                {
                    respuesta.Datos = result;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("RegistrarProducto")]
        [HttpPost]
        public IActionResult RegistrarProducto(Producto entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var result = db.Execute("RegistrarProducto",
                    new { entidad.NombreProducto, entidad.Inventario, entidad.IdCategoria },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Este producto ya se encuentra registrado.";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ActualizarProducto")]
        [HttpPut]
        public IActionResult ActualizarProducto(Producto entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var result = db.Execute("ActualizarProducto",
                    new { entidad.IdProducto, entidad.NombreProducto, entidad.Inventario, entidad.IdCategoria },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Este producto no se pudo actualizar.";
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("EliminarProducto")]
        [HttpDelete]
        public IActionResult EliminarProducto(long IdProducto)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var result = db.Execute("EliminarProducto",
                    new { IdProducto },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Este producto no se pudo eliminar.";
                }

                return Ok(respuesta);
            }
        }

    }
}