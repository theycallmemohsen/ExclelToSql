using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public DutyController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Duties.ToListAsync();

            return Ok(data);
        }
    }
}