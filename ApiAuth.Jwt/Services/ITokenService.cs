using ApiAuth.Jwt.Models;
using System.Security.Claims;

namespace ApiAuth.Jwt.Services
{
    public interface ITokenService
    {
        TokenModel GenerateToken(string username);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
