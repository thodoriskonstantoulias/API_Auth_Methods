using System.ComponentModel.DataAnnotations;

namespace ApiAuth.Jwt.Models
{
    public class UserRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
