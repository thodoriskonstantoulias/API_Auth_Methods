namespace ApiAuth.Jwt.Models
{
    public class TokenModel : ResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
