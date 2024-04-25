using API_Progra_AvanzadaW.Entidades;
using API_Progra_AvanzadaW.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Dapper;


namespace API_Progra_AvanzadaW.Controllers
{
    [Route("api/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController(IConfiguration _configuration, IUtilitariosModel _utilitariosModel) : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                long IdUsuario = long.Parse(_utilitariosModel.Decrypt(User.Identity!.Name!));
                string Descripcion = context!.Error.Message;
                string Origen = context!.Path;

                db.Execute("RegistrarBitacora",
                    new { IdUsuario, Descripcion, Origen },
                    commandType: CommandType.StoredProcedure);
            }

            return Problem(
                 detail: context!.Error.StackTrace,
                 title: context!.Error.Message);
        }
    }
}
