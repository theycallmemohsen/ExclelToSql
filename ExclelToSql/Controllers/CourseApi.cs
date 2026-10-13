using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApi : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public CourseApi(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Courses.ToListAsync();

            return Ok(data);
        }
    }
}