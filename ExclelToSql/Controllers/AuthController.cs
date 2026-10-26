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
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName == request.Username &&
                x.PassWord1 == request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            HttpContext.Session.SetString("Username", user.UserName);
            HttpContext.Session.SetString("Role", user.Auth);
            HttpContext.Session.SetString("Last", user.LastName);


            return Ok(new
            {
                message = "Login successful",
                user.UserName,
                user.Auth,
                user.LastName

            });
        }
        [HttpGet("Me")]
        public IActionResult Me()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("Role");
            var lastname = HttpContext.Session.GetString("Last");


            if (username == null)
                return Unauthorized();

            return Ok(new
            {
                Username = username,
                Role = role,
                LastName = lastname
            });
        }
        [HttpGet("SessionTest")]
        public IActionResult SessionTest()
        {
            HttpContext.Session.SetString("SessionTest", "working");

            return Ok(new
            {
                SessionId = HttpContext.Session.Id,
                TestValue = HttpContext.Session.GetString("SessionTest")
            });
        }
    }

}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ExclelToSql.Data;
//using ExclelToSql.Models.DTO;

//namespace ExclelToSql.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly ExclelToSqlContext _context;

//        public AuthController(ExclelToSqlContext context)
//        {
//            _context = context;
//        }

//        [HttpPost("Login")]
//        public async Task<IActionResult> Login(LoginRequest request)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(x =>
//                x.UserName == request.Username &&
//                x.PassWord1 == request.Password);

//            if (user == null)
//            {
//                return Unauthorized("Invalid username or password.");
//            }

//            HttpContext.Session.SetString("Username", user.UserName);
//            HttpContext.Session.SetString("Role", user.Auth ?? "");
//            HttpContext.Session.SetString("Last", user.LastName ?? "");

//            return Ok(new
//            {
//                message = "Login successful",
//                userName = user.UserName,
//                auth = user.Auth,
//                lastName = user.LastName
//            });
//        }

//        [HttpGet("Me")]
//        public IActionResult Me()
//        {
//            var username = HttpContext.Session.GetString("Username");
//            var role = HttpContext.Session.GetString("Role");
//            var lastName = HttpContext.Session.GetString("Last");

//            if (string.IsNullOrEmpty(username))
//            {
//                return Unauthorized();
//            }

//            return Ok(new
//            {
//                username,
//                role,
//                lastName
//            });
//        }

//        [HttpPost("Logout")]
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return Ok(new { message = "Logged out successfully" });
//        }
//    }
//}