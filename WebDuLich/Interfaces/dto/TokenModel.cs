using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
	public class TokenModel
	{
		[Required]
		public string AccessToken { get; set; }

		[Required]
		public string RefreshToken { get; set; }

		public DateTime AccessTokenExpires { get; set; }
		public DateTime RefreshTokenExpires { get; set; }
		public string TokenType { get; set; } = "Bearer";
	}
}
