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

        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Connect([FromQuery] UserRequest userRequest)
        {
            var model = this.tokenService.GenerateToken(userRequest);
            if (!model.Success)
            {
                return Unauthorized(model);
            }

            return Ok(model);
        }
    }
}
