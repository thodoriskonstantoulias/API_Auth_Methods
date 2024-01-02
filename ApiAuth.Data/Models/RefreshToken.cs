using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAuth.Data.Models
{
    public class UserRefreshToken
    {
        public int ID { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? RefreshToken { get; set; }

        public bool IsActive { get; set; } = true;

        public bool Revoked { get; set; }

        [Required]
        public DateTime? ExpiredDate { get; set; }
    }
}
