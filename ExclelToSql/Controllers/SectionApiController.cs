using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public SectionApiController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Sections.ToListAsync();

            return Ok(data);
        }
    }
}
