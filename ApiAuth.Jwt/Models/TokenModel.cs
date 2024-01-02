using System.ComponentModel.DataAnnotations;

namespace ApiAuth.Jwt.Models
{
    public class TokenModel : ResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class RefreshTokenModel 
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        public string? RefreshToken { get; set; }
    }
}
