using Microsoft.AspNetCore.Mvc;
using API_Progra_AvanzadaW.Entidades;
using Microsoft.AspNetCore.Authorization;
using API_Progra_AvanzadaW.Services;
using API_Progra_AvanzadaW.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace API_Progra_AvanzadaW.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IUtilitariosModel _utilitariosModel;
		private readonly IConfiguration _configuration;
        public UsuarioController(IUtilitariosModel utilitariosModel, IConfiguration configuration)
        {
            _utilitariosModel = utilitariosModel;
            _configuration = configuration;
        }

		[AllowAnonymous]
		[Route("IniciarSesion")]
		[HttpPost]
		public IActionResult IniciarSesion(Usuario entidad)
		{
			return Ok();
		}

		[AllowAnonymous]
		[Route("RegistrarUsuario")]
		[HttpPost]
		public IActionResult RegistrarUsuario(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return Ok(db.Execute("RegistrarUsuario",
                    new { entidad.Correo, entidad.Contrasenna, entidad.Nombre },
                    commandType: CommandType.StoredProcedure));
            }
        }
    }
}