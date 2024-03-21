using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Services
{
        public interface IProductoModel
        {
            ProductoRespuesta? ConsultarProductos();
            CategoriaRespuesta? ConsultarCategorias();
            Respuesta? RegistrarProducto(Producto entidad);
            Respuesta? EliminarProducto(long IdProducto);
        }
    
}