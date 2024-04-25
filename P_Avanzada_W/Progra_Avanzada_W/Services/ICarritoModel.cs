using Progra_Avanzada_W.Entidades;
using static Progra_Avanzada_W.Entidades.FacturasEnt;

namespace Progra_Avanzada_W.Services
{
    public interface ICarritoModel
    {
        Respuesta? RegistrarCarrito(CarritoEnt entidad);
        List<CarritoEnt>? ConsultarCarrito();
        Respuesta? PagarCarrito();
        Respuesta? EliminarProductoCarrito(long q);
        FacturaRespuesta? ConsultarFacturas();
        FacturaRespuesta? ConsultarDetalleFactura(long q);
    }
}
