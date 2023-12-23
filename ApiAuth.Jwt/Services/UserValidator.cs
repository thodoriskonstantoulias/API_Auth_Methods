using ApiAuth.Jwt.Models;

namespace ApiAuth.Jwt.Services
{
    public class UserValidator : IUserValidator
    {
        private readonly IConfiguration configuration;

        public UserValidator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsUserValidFromSettings(UserRequest userRequest)
        {
            var validUsername = this.configuration.GetValue<string>("Jwt:TestUsername");
            var validPassword = this.configuration.GetValue<string>("Jwt:TestPassword");
            if (validUsername != userRequest.Username || validPassword != userRequest.Password)
            {
                return false;
            }

            return true;
        }
    }
}
