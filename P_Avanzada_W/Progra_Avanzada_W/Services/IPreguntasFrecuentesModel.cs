using Progra_Avanzada_W.Entidades;

namespace Progra_Avanzada_W.Services
{
    public interface IPreguntasFrecuentesModel
    {
        public int RegistrarPreguntaFrecuente(PreguntasFrecuentes entidad);

        public List<PreguntasFrecuentes>? ObtenerPreguntaFrecuentePorId(long q);

        public int ActualizarPreguntasFrecuentes(PreguntasFrecuentes entidad);

        public List<PreguntasFrecuentes>? ObtenerTodasLasPreguntasFrecuentes();
        public int EliminarPreguntaFrecuentePorId(long q);
    }
}
