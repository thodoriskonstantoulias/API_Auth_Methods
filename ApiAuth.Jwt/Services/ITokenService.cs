using ApiAuth.Jwt.Models;

namespace ApiAuth.Jwt.Services
{
    public interface ITokenService
    {
        TokenModel GenerateToken(UserRequest userRequest); 
    }
}
