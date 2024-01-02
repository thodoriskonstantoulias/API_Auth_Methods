using ApiAuth.Data.Models;
using ApiAuth.Jwt.Models;

namespace ApiAuth.Jwt.Services
{
    public interface IAccountService
    {
        Task<ResponseModel> RegisterUserAsync(UserRequest userRequest);

        Task<ResponseModel> LoginUserFromSettings(UserRequest userRequest);

        Task<ResponseModel> LoginUserAsync(UserRequest userRequest);

        Task AddUserRefreshTokenAsync(string token, string? username);

        Task<UserRefreshToken?> GetRefreshTokenAsync(string token, string? username);

        Task<List<UserRefreshToken>> GetUserActiveRefreshTokensAsync(string? username);
    }
}
