namespace API_Progra_AvanzadaW.Entidades
{
    public class Respuesta
    {
        public Respuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
