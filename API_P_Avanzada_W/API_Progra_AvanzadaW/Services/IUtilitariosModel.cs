namespace API_Progra_AvanzadaW.Services
{
	public interface IUtilitariosModel
	{
        string Encrypt(string texto);

        string? GenerarToken(string Correo);

        string GenerarCodigo();

        void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);

    }
}