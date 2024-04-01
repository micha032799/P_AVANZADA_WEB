using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Services
{
        public interface IProductoModel
        {
        ProductoRespuesta? ConsultarProductos();
        Respuesta? EditarProducto(Producto entidad);
        Respuesta? RegistrarProducto(Producto entidad);
        Respuesta? EliminarProductoPorId(long IdProducto);
    }
    
}