using ExclelToSql.Data;
using ExclelToSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersSemesterApi : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public TeachersSemesterApi(ExclelToSqlContext context)
        {
            _context = context;
        }

        // GET api/TeachersSemesterApi
        // Optional filters: ?teacherId=0010233881  and/or  ?semesterCode=2
        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] string? teacherId, [FromQuery] int? semesterCode)
        //{
        //    var query = _context.Teachers
        //        .AsNoTracking()
        //        .SelectMany(t => t.SemesterCodes, (t, s) => new
        //        {
        //            TeacherId = t.PersonalCode,
        //            SemesterCode = s.Id
        //        });

        //    if (!string.IsNullOrWhiteSpace(teacherId))
        //        query = query.Where(x => x.TeacherId == teacherId);

        //    if (semesterCode.HasValue)
        //        query = query.Where(x => x.SemesterCode == semesterCode.Value);

        //    var rows = await query.ToListAsync();

        //    var data = rows
        //        .GroupBy(x => x.TeacherId)
        //        .Select(g => new
        //        {
        //            TeacherId = g.Key,
        //            SemesterCodes = g.Select(x => x.SemesterCode).Distinct().OrderBy(c => c).ToList()
        //        })
        //        .OrderBy(x => x.TeacherId)
        //        .ToList();

        //    return Ok(data);
        //}

        /// //////////////////////////////////////////////////////////////////////////////////////

        //GET /api/TeachersSemesterApi?expand=semester
        [HttpGet]
        public async Task<IActionResult> GetAll(
    [FromQuery] string? teacherId,
    [FromQuery] int? semesterCode,
    [FromQuery] string? expand)          // NEW: ?expand=semester  (or "all")
        {
            var ex = (expand ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => x.ToLower()).ToList();
            bool includeSemester = ex.Contains("semester") || ex.Contains("all");

            var query = _context.Teachers
                .AsNoTracking()
                .SelectMany(t => t.SemesterCodes, (t, s) => new
                {
                    TeacherId = t.PersonalCode,
                    SemesterId = s.Id,
                    // Semester's own columns, read only when requested
                    SemesterInfo = includeSemester
                        ? new
                        {
                            s.Id,
                            s.SemesterCode,      // Semester's own SemesterCode column
                            s.Title
                            // add any other Semester columns here
                        }
                        : null
                });

            if (!string.IsNullOrWhiteSpace(teacherId))
                query = query.Where(x => x.TeacherId == teacherId);

            if (semesterCode.HasValue)
                query = query.Where(x => x.SemesterId == semesterCode.Value);

            var rows = await query.ToListAsync();

            var data = rows
                .GroupBy(x => x.TeacherId)
                .Select(g => new
                {
                    TeacherId = g.Key,
                    SemesterCodes = g.Select(x => x.SemesterId).Distinct().OrderBy(c => c).ToList(),

                    // only filled when expand=semester, otherwise null
                    Semesters = includeSemester
                        ? g.Select(x => x.SemesterInfo!)
                           .GroupBy(s => s.Id)
                           .Select(sg => sg.First())
                           .OrderBy(s => s.Id)
                           .ToList()
                        : null
                })
                .OrderBy(x => x.TeacherId)
                .ToList();

            return Ok(data);
        }
        // /////////////////////////////////////////////////////////////////////////////////////////////////

        // POST api/TeachersSemesterApi   body: { "teacherId": "70823153", "semesterCode": 2 }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeacherSemesterDto dto)
        {
            var teacher = await _context.Teachers
                .Include(t => t.SemesterCodes)
                .FirstOrDefaultAsync(t => t.PersonalCode == dto.TeacherId);
            if (teacher == null) return NotFound("Teacher not found.");

            var semester = await _context.Semesters.FindAsync(dto.SemesterCode);
            if (semester == null) return NotFound("Semester not found.");

            if (teacher.SemesterCodes.Any(s => s.Id == semester.Id))
                return Conflict("This teacher is already linked to that semester.");

            teacher.SemesterCodes.Add(semester);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}