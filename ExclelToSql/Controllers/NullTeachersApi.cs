using ExclelToSql.Data;
using ExclelToSql.DTOs;
using ExclelToSql.Models;
using ExclelToSql.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NullTeachersApi : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public NullTeachersApi(ExclelToSqlContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Nullteachers.ToListAsync();

            return Ok(data);
        }

        //"api/NullTeachersApi/{NationalCode}"
        [HttpPatch("{nationalCode}")]
        public async Task<IActionResult> Patch(
            string nationalCode,
            [FromBody] UpdateNullTeacherDto request)
        {
            var nullTeacher = await _context.Nullteachers
                .FirstOrDefaultAsync(t => t.NationalCode == nationalCode);

            if (nullTeacher == null)
                return NotFound("Teacher not found in NullTeachers.");

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.NationalCode == nationalCode);

            if (teacher == null)
                return NotFound("Teacher not found in Teachers.");

            nullTeacher.Duty = request.Duty;
            nullTeacher.Ranking = request.Ranking;
            nullTeacher.Grade = request.Grade;

            teacher.Duty = request.Duty;
            teacher.Ranking = request.Ranking;
            teacher.Grade = request.Grade;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Both tables updated successfully.",
                nationalCode,
                request.Duty,
                request.Ranking,
                request.Grade
            });
        }
    }
}
