using Microsoft.IdentityModel.Tokens;
using API_Progra_AvanzadaW.Services;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Progra_AvanzadaW.Models
{
	public class UtilitariosModel : IUtilitariosModel
	{
		private readonly IConfiguration _configuration;

		public UtilitariosModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerarToken(string Cedula)
		{
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim("username", Cedula));

			string SecretKey = _configuration.GetSection("settings:SecretKey").Value ?? string.Empty;
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(15),
				signingCredentials: cred);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}