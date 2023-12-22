namespace ApiAuth.Jwt.Models
{
    public class TokenModel
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
