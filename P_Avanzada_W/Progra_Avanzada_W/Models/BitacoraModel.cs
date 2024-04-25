using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;
using System.Net.Http.Headers;

namespace Progra_Avanzada_W.Models
{
    public class BitacoraModel : IBitacoraModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _urlApi;

        public BitacoraModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _HttpContextAccessor = httpContextAccessor;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
        }

        public void RegistrarErrorBitacora(BitacoraEnt entidad)
        {
            string url = _urlApi + "api/Bitacora/RegistrarErrorBitacora";
            
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;
        }
    }
}
