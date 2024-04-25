﻿namespace Progra_Avanzada_W.Entidades
{
    public class CarritoEnt
    {
        public long IdCarrito { get; set; }
        public long IdUsuario { get; set; }
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string nomUsuario { get; set; } = string.Empty;
        public string nomProducto { get; set; } = string.Empty;
    }

}
