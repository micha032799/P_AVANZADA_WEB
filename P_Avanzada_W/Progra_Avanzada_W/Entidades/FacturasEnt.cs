namespace Progra_Avanzada_W.Entidades
{
    public class FacturasEnt
    {
        public long IdFactura { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal TotalPago { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal Total { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public class FacturaRespuesta
        {
            public FacturaRespuesta()
            {
                Codigo = "00";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public FacturasEnt? Dato { get; set; }
            public List<FacturasEnt>? Datos { get; set; }
        }
    }
}
