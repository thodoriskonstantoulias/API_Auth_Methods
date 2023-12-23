using ApiAuth.Jwt.Models;
using ApiAuth.Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            var model = this.tokenService.GenerateToken(userRequest);

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

            var model = this.tokenService.GenerateToken(userRequest);

            return Ok(model);
        }
    }
}
