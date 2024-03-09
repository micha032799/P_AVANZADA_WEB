﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using API_Progra_AvanzadaW.Entidades;

namespace API_Progra_AvanzadaW.Entidades
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
    }
}
