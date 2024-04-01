﻿using NuGet.Common;
using Progra_Avanzada_W.Services;
using Progra_Avanzada_W.Entidades;
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

        public Respuesta? RegistrarProducto(Producto entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/RegistrarProducto";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public Respuesta? EditarProducto(Producto entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/EditarProducto";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public Respuesta? EliminarProductoPorId(long IdProducto)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/EliminarProductoPorId?IdProducto=" + IdProducto;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }
    }
}