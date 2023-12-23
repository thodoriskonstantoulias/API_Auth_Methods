using ApiAuth.Jwt.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiAuth.Jwt.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<ResponseModel> LoginUserAsync(UserRequest userRequest)
        {
            var user = await this.userManager.FindByEmailAsync(userRequest.Username!);
            if (user == null)
            {
                return new ResponseModel
                {
                    Success = false,
                    ErrorMessage = "Username or password is incorrect"
                };
            }

            var userIsValid = await this.userManager.CheckPasswordAsync(user, userRequest.Password!);
            if (!userIsValid)
            {
                return new ResponseModel
                {
                    Success = false,
                    ErrorMessage = "Username or password is incorrect"
                };
            }

            return new ResponseModel
            {
                Success = true
            };
        }

        public Task<ResponseModel> LoginUserFromSettings(UserRequest userRequest)
        {
            var validUsername = this.configuration.GetValue<string>("Jwt:TestUsername");
            var validPassword = this.configuration.GetValue<string>("Jwt:TestPassword");
            if (validUsername != userRequest.Username || validPassword != userRequest.Password)
            {
                return Task.FromResult(new ResponseModel
                {
                    Success = false,
                    ErrorMessage = "Username or password is incorrect"
                });
            }

            return Task.FromResult(new ResponseModel
            {
                Success = true
            });
        }

        public async Task<ResponseModel> RegisterUserAsync(UserRequest userRequest)
        {
            var userExists = await this.userManager.FindByEmailAsync(userRequest.Username!);
            if (userExists != null)
            {
                return new ResponseModel
                {
                    Success = false,
                    ErrorMessage = "User exists"
                };
            }

            IdentityUser user = new()
            {
                Email = userRequest.Username,
                UserName = userRequest.Username
            };

            var result = await this.userManager.CreateAsync(user, userRequest.Password!);
            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Success = false,
                    ErrorMessage = "User creation failed!"
                };
            }

            return new ResponseModel
            {
                Success = true
            };
        }
    }
}
