namespace Progra_Avanzada_W.Entidades
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public float precio { get; set; }
        public int Cantidad { get; set; }
        public string imagen { get; set; } = string.Empty;
    }

    public class ProductoRespuesta
    {
        public ProductoRespuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public Producto? Dato { get; set; }
        public List<Producto>? Datos { get; set; }
    }
}
