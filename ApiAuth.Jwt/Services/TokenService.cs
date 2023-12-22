using ApiAuth.Jwt.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAuth.Jwt.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TokenModel GenerateToken(UserRequest userRequest)
        {
            var validUsername = this.configuration.GetValue<string>("Jwt:TestUsername");
            var validPassword = this.configuration.GetValue<string>("Jwt:TestPassword");
            if (validUsername != userRequest.Username || validPassword != userRequest.Password)
            {
                return new TokenModel
                {
                    Success = false,
                    ErrorMessage = "Username or password is incorrect"
                };
            }

            var issuer = this.configuration.GetValue<string>("Jwt:Issuer");
            var audience = this.configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(this.configuration.GetValue<string>("Jwt:Key")!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, userRequest.Username!),
                    new Claim(JwtRegisteredClaimNames.Email, userRequest.Username!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new TokenModel
            {
                Success = true,
                Token = jwtToken
            };
        }
    }
}
