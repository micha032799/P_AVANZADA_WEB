using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Services
{
    public interface IUsuarioModel
    {
        Respuesta? RegistrarUsuario(Usuario entidad);

        UsuarioRespuesta? IniciarSesion(Usuario entidad);

        UsuarioRespuesta? RecuperarAcceso(Usuario entidad);

        UsuarioRespuesta? CambiarContrasenna(Usuario entidad);
    }
}
