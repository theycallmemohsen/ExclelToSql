using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassApi : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public ClassApi(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Classes.ToListAsync();

            return Ok(data);
        }
    }
}