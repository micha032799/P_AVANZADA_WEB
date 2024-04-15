using Microsoft.AspNetCore.Http;
using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;

namespace Progra_Avanzada_W.Models
{
    public class ReservaModel(HttpClient _http,
                                IConfiguration _configuration,
                                IHttpContextAccessor _sesion) : IReservaModel
    {
        public Respuesta? RegistrarReserva(Reserva entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Reserva/RegistrarReserva";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public Respuesta? ActualizarReserva(Reserva entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Reserva/ActualizarReserva";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public ReservaRespuesta? ConsultarReservas()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Reserva/ConsultarReservas";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ReservaRespuesta>().Result;

            return null;
        }

        public ReservaRespuesta? ConsultarReserva(long IdReserva)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Reserva/ConsultarReserva?IdReserva=" + IdReserva;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ReservaRespuesta>().Result;

            return null;
        }

        public ReservaRespuesta? EliminarReserva(long IdReserva)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Reserva/EliminarReserva?IdReserva=" + IdReserva;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ReservaRespuesta>().Result;

            return null;
        }

    }
}