namespace API_Progra_AvanzadaW.Entidades
{
    public class PreguntasFrecuentes
    {

        public long IdPreguntasFrecuentes { get; set; }

        public long IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Pregunta { get; set; } = string.Empty;

        public string Respuesta { get; set; } = string.Empty;
    }
}
