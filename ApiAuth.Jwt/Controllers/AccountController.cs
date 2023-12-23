using ApiAuth.Jwt.Models;
using ApiAuth.Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Jwt.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ITokenService tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            this.accountService = accountService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRequest userRequest)
        {
            var response = await this.accountService.RegisterUserAsync(userRequest);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            var model = this.tokenService.GenerateToken(userRequest);
            return Ok(model);
        }
    }
}
