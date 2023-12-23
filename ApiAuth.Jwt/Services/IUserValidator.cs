using ApiAuth.Jwt.Models;

namespace ApiAuth.Jwt.Services
{
    public interface IUserValidator
    {
        bool IsUserValidFromSettings(UserRequest userRequest);
    }
}
