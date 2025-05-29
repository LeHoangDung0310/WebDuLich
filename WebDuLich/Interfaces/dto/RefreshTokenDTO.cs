using System.ComponentModel.DataAnnotations;

namespace WebDuLich.Interfaces.dto
{
    public class RefreshTokenDTO
    {
        [Required(ErrorMessage = "Access token is required")]
        public string AccessToken { get; set; }
        
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; }
    }
}