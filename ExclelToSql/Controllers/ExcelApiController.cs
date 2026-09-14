using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public ExcelApiController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Excels.ToListAsync();

            return Ok(data);
        }
    }
}