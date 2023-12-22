using ApiAuth.Jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Jwt.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        // test here - real world from db
        private static readonly Employee[] Employees = new[]
        {
            new Employee{ Name = "Ted", JobTitle = "Back End Developer"},
            new Employee{ Name = "Kostas", JobTitle = "Front End Developer"},
            new Employee{ Name = "John", JobTitle = "DB Admin"}
        };

        public PeopleController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(Employees);
        }
    }
}
