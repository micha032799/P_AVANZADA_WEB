﻿using Progra_Avanzada_W.Entidades;
using Progra_Avanzada_W.Services;

namespace Progra_Avanzada_W.Models
{
    public class UsuarioModel(HttpClient _http, IConfiguration _configuration) : IUsuarioModel
    {
        public Respuesta? RegistrarUsuario(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? IniciarSesion(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/IniciarSesion";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? RecuperarAcceso(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RecuperarAcceso";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? CambiarContrasenna(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/CambiarContrasenna";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

    }
}
