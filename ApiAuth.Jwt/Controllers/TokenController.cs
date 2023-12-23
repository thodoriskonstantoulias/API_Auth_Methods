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
        private readonly IUserValidator userValidator;

        public TokenController(ITokenService tokenService, IUserValidator userValidator)
        {
            this.tokenService = tokenService;
            this.userValidator = userValidator;
        }

        [HttpGet]
        public IActionResult Connect([FromQuery] UserRequest userRequest)
        {
            if (!this.userValidator.IsUserValidFromSettings(userRequest))
            {
                return BadRequest("Username or password is incorrect");
            }

            var model = this.tokenService.GenerateToken(userRequest);
            if (!model.Success)
            {
                return Unauthorized(model);
            }

            return Ok(model);
        }
    }
}
