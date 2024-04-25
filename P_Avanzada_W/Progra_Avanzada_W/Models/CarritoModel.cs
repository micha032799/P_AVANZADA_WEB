using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using static Progra_Avanzada_W.Entidades.FacturasEnt;

namespace Progra_Avanzada_W.Models
{
    public class CarritoModel(HttpClient _http, IConfiguration _configuration,
                               IHttpContextAccessor _sesion) : ICarritoModel
    {
        private string _urlApi;

        public Respuesta? RegistrarCarrito(CarritoEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/RegistrarCarrito";

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            JsonContent body = JsonContent.Create(entidad);

            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public List<CarritoEnt>? ConsultarCarrito()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/ConsultarCarrito";
            string test = _sesion.HttpContext?.Session.GetString("Token");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<CarritoEnt>?>().Result;
            else
                return null;
        }

        public Respuesta? PagarCarrito()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/PagarCarrito";

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            var resp = _http.PostAsync(url, null).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public Respuesta? EliminarProductoCarrito(long q)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/EliminarProductoCarrito?q=" + q;

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            var resp = _http.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public FacturaRespuesta? ConsultarFacturas()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/ConsultarFacturas";

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<FacturaRespuesta>().Result;

            return null;
        }

        public FacturaRespuesta? ConsultarDetalleFactura(long q)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Carrito/ConsultarDetalleFactura?q=" + q;

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));

            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<FacturaRespuesta>().Result;

            return null;
        }
    }
}
