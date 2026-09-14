using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;
using ExclelToSql.Models.DTO;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public AuthController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Tests.FirstOrDefaultAsync(x =>
                x.UserName == request.Username &&
                x.PassWord1 == request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            HttpContext.Session.SetString("Username", user.UserName);
            HttpContext.Session.SetString("Role", user.Auth);

            return Ok(new
            {
                message = "Login successful",
                user.UserName,
                user.Auth
            });
        }
        [HttpGet("Me")]
        public IActionResult Me()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");

            if (username == null)
                return Unauthorized();

            return Ok(new
            {
                Username = username,
                Role = role
            });
        }
    }
}