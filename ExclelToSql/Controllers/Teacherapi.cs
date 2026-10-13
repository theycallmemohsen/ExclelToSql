using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Teacherapi : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public Teacherapi(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Teachers.ToListAsync();

            return Ok(data);
        }
    }
}