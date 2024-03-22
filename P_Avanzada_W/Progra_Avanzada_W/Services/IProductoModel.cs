using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Services
{
        public interface IProductoModel
        {
        ProductoRespuesta? ConsultarProductos();
        ProductoRespuesta? ConsultarProducto(long IdProducto);
        CategoriaRespuesta? ConsultarCategorias();
        Respuesta? RegistrarProducto(Producto entidad);
        Respuesta? ActualizarProducto(Producto entidad);
        Respuesta? EliminarProducto(long IdProducto);
        }
    
}