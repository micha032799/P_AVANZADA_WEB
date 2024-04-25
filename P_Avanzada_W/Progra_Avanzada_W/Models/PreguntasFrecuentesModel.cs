using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;
using System.Net.Http.Headers;

namespace Progra_Avanzada_W.Models
{
    public class PreguntasFrecuentesModel(HttpClient _http, IConfiguration _configuration,
                               IHttpContextAccessor _sesion) : IPreguntasFrecuentesModel
    {
        public int RegistrarPreguntaFrecuente(PreguntasFrecuentes entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value +"api/PreguntasFrecuentes/RegistrarPreguntaFrecuente";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        public List<PreguntasFrecuentes>? ObtenerPreguntaFrecuentePorId(long q)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PreguntasFrecuentes/ObtenerPreguntaFrecuentePorId?q=" + q; 
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<PreguntasFrecuentes>>().Result;
            else
                return null;
        }
        public int ActualizarPreguntasFrecuentes(PreguntasFrecuentes entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PreguntasFrecuentes/ActualizarPreguntasFrecuentes";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
        public List<PreguntasFrecuentes>? ObtenerTodasLasPreguntasFrecuentes()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PreguntasFrecuentes/ObtenerTodasLasPreguntasFrecuentes";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<PreguntasFrecuentes>>().Result;
            else
                return null;
        }

        public int EliminarPreguntaFrecuentePorId(long q)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PreguntasFrecuentes/EliminarPreguntaFrecuentePorId?q=" + q;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }
    }
}
