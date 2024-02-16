using Progra_Avanzada_W.Services;

namespace Progra_Avanzada_W.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        public UsuarioModel(HttpClient http)
        {
            _http = http;
            _configuration = configuration;
        }

        public int RegistrarUsuario(Usuario entidad)
        {
            string urlApi = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;

            return 0;   
        }
    }
}
