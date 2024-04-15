using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Models
{
    public interface IReservaModel
    {
        Respuesta? RegistrarReserva(Reserva entidad);
        Respuesta? ActualizarReserva(Reserva entidad);
        ReservaRespuesta? EliminarReserva(long IdReserva);
        ReservaRespuesta? ConsultarReservas();
        ReservaRespuesta? ConsultarReserva(long IdReserva);
    }
}
