using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;
using System.Net.Http.Headers;



namespace Progra_Avanzada_W.Models
{
    public class ProductoModel(HttpClient _http, IConfiguration _configuration,
                               IHttpContextAccessor _sesion) : IProductoModel
    {
        public ProductoRespuesta? ConsultarProductos()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/ConsultarProductos";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ProductoRespuesta>().Result;

            return null;
        }
    }
}
