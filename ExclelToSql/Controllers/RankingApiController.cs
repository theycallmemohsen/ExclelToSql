using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public RankingApiController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Rankings.ToListAsync();

            return Ok(data);
        }
    }
}