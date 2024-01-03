using ApiAuth.Jwt.Models;
using ApiAuth.Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiAuth.Jwt.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IAccountService accountService;

        public TokenController(ITokenService tokenService, IAccountService accountService)
        {
            this.tokenService = tokenService;
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Connect([FromQuery] UserRequest userRequest)
        {
            var loginModel = await this.accountService.LoginUserFromSettings(userRequest);
            if (!loginModel.Success)
            {
                return BadRequest(loginModel);
            }

            var model = this.tokenService.GenerateToken(userRequest.Username!);

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConnectWithIdentity([FromQuery] UserRequest userRequest)
        {
            var loginModel = await this.accountService.LoginUserAsync(userRequest);
            if (!loginModel.Success)
            {
                return BadRequest(loginModel);
            }

            var model = this.tokenService.GenerateToken(userRequest.Username!);
            await this.accountService.AddUserRefreshTokenAsync(model.RefreshToken!, userRequest.Username!);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel request)
        {
            var accessToken = request.Token;
            var refreshToken = request.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken!);
            // var username = principal.Identity?.Name; //this is mapped to the Name claim by default
            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; 

            var dbRefreshToken = await this.accountService.GetRefreshTokenAsync(refreshToken!, username);
            if (dbRefreshToken == null)
            {
                return BadRequest("Refresh token does not exist");
            }

            if (!dbRefreshToken.IsActive)
            {
                return BadRequest("Refresh token is not active");
            }

            if (dbRefreshToken.Revoked)
            {
                return BadRequest("Refresh token is revoked");
            }

            if (dbRefreshToken.ExpiredDate <= DateTime.UtcNow)
            {
                return BadRequest("Refresh token is expired");
            }

            var model = this.tokenService.GenerateToken(username!);
            await this.accountService.AddUserRefreshTokenAsync(model.RefreshToken!, username!);

            return Ok(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] string refreshToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var dbRefreshToken = await this.accountService.GetRefreshTokenAsync(refreshToken!, username);
            if (dbRefreshToken == null)
            {
                return BadRequest("Refresh token does not exist");
            }

            if (!dbRefreshToken.IsActive)
            {
                return BadRequest("Refresh token is not active");
            }

            if (dbRefreshToken.Revoked)
            {
                return BadRequest("Refresh token is already revoked");
            }

            await this.accountService.RevokeRefreshTokenAsync(refreshToken!, username);

            return NoContent();
        }
    }
}
