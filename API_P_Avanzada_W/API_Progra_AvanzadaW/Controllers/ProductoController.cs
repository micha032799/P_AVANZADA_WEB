using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API_Progra_AvanzadaW.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        [Route("ConsultarProductos")]
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest("Error al consultar productos: " + ex.Message);
            }
        }

        [Authorize]
        [Route("EditarProducto")]
        [HttpPut]
        public IActionResult EditarProducto(Producto entidad)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    ProductoRespuesta respuesta = new ProductoRespuesta();
                    var datos = db.Execute("EditarProducto",
                        new
                        {
                            entidad.IdProducto,
                            entidad.nombre,
                            entidad.descripcion,
                            entidad.precio,
                            entidad.imagen,
                            entidad.Cantidad
                        },
                        commandType: CommandType.StoredProcedure);

                    if (datos > 0)
                    {
                        respuesta.Codigo = "0";
                        respuesta.Mensaje = "Producto actualizado correctamente.";
                    }
                    else
                    {
                        respuesta.Codigo = "-1";
                        respuesta.Mensaje = "No se pudo actualizar el producto.";
                    }

                    return Ok(respuesta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al editar el producto: " + ex.Message);
            }
        }

        [Authorize]
        [Route("RegistrarProducto")]
        [HttpPost]
        public IActionResult RegistrarProducto(Producto entidad)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    Respuesta respuesta = new Respuesta();

                    var result = db.Execute("RegistrarProducto",
                         new { entidad.nombre, entidad.descripcion, entidad.precio, entidad.imagen, entidad.Cantidad},
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        respuesta.Codigo = "-1";
                        respuesta.Mensaje = "Este producto ya se encuentra registrado.";
                    }

                    return Ok(respuesta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al registrar el producto: " + ex.Message);
            }
        }

        [Authorize]
        [Route("EliminarProductoPorId")]
        [HttpDelete]
        public IActionResult EliminarProductoPorId(long IdProducto)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    Respuesta respuesta = new Respuesta();

                    var result = db.Execute("EliminarProductoPorId",
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
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el producto: " + ex.Message);
            }
        }

    }
}
