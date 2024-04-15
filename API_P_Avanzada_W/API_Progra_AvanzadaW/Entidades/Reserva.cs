namespace API_Progra_AvanzadaW.Entidades
{
    public class Reserva
    {
        public long IdReserva { get; set; }
        public long IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public int CantidadPersonas { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaFinReserva { get; set; }
        public decimal Precio { get; set; }
        public string? EstadoReserva { get; set; }
    }

    public class ReservaRespuesta
    {
        public ReservaRespuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public Reserva? Dato { get; set; }
        public List<Reserva>? Datos { get; set; }
    }
}