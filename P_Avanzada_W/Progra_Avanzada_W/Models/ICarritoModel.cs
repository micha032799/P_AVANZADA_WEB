using Progra_Avanzada_W.Entities;

namespace Progra_Avanzada_W.Models
{
    public interface ICarritoModel
    {
        public long RegistrarCarrito(CarritoEnt entidad);

        public List<CarritoEnt>? ConsultarCarrito();

        public string PagarCarrito();

        public int EliminarProductoCarrito(long q);

        public List<FacturasEnt>? ConsultarFacturas();

        public List<FacturasEnt>? ConsultarDetalleFactura(long q);
    }
}
