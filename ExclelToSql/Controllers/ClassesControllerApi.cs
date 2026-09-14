using ExclelToSql.Data;
using ExclelToSql.Models;
using ExclelToSql.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public ClassesApiController(ExclelToSqlContext context)
        {
            _context = context;
        }


        // GET: api/ClassesApi
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? expand)
        {
            IQueryable<Class> query = _context.Classes;

            bool includeCourse = false;
            bool includeTeacher = false;
            bool includeSection = false;


            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeCourse = expands.Contains("courseid") || expands.Contains("all");
                includeTeacher = expands.Contains("teacherid") || expands.Contains("all");
                includeSection = expands.Contains("section") || expands.Contains("all");
            }


            if (includeCourse)
                query = query.Include(c => c.Course);

            if (includeTeacher)
                query = query.Include(c => c.Teacher);

            if (includeSection)
                query = query.Include(c => c.SectionNavigation);



            var result = await query
                .Select(c => new
                {
                    c.Id,
                    c.CourseId,
                    c.TeacherId,
                    c.Section,
                    c.StudentsCount,


                    Course = includeCourse && c.Course != null
                        ? new
                        {
                            c.Course.Id,
                            c.Course.CourseId,
                            c.Course.CourseName,
                            c.Course.Theoretical,
                            c.Course.Practical,
                            c.Course.CreditsCoefficients
                        }
                        : null,


                    Teacher = includeTeacher && c.Teacher != null
                        ? new
                        {
                            c.Teacher.Id,
                            c.Teacher.PersonalCode,
                            c.Teacher.FirstName,
                            c.Teacher.LastName,
                            c.Teacher.AccountNumber,
                            c.Teacher.InsuranceNumber,
                            c.Teacher.NationalCode,
                            c.Teacher.Grade,
                            c.Teacher.Ranking
                        }
                        : null,


                    SectionInfo = includeSection && c.SectionNavigation != null
                        ? new
                        {
                            c.SectionNavigation.Id,
                            c.SectionNavigation.Section1
                        }
                        : null

                })
                .ToListAsync();


            return Ok(result);
        }




        // GET: api/ClassesApi/14
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string? expand)
        {
            IQueryable<Class> query = _context.Classes;

            bool includeCourse = false;
            bool includeTeacher = false;
            bool includeSection = false;


            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeCourse = expands.Contains("courseid") || expands.Contains("all");
                includeTeacher = expands.Contains("teacherid") || expands.Contains("all");
                includeSection = expands.Contains("section") || expands.Contains("all");
            }


            if (includeCourse)
                query = query.Include(c => c.Course);

            if (includeTeacher)
                query = query.Include(c => c.Teacher);

            if (includeSection)
                query = query.Include(c => c.SectionNavigation);



            var classResult = await query
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.CourseId,
                    c.TeacherId,
                    c.Section,
                    c.StudentsCount,


                    Course = includeCourse && c.Course != null
                        ? new
                        {
                            c.Course.Id,
                            c.Course.CourseId,
                            c.Course.CourseName,
                            c.Course.Theoretical,
                            c.Course.Practical,
                            c.Course.CreditsCoefficients
                        }
                        : null,


                    Teacher = includeTeacher && c.Teacher != null
                        ? new
                        {
                            c.Teacher.Id,
                            c.Teacher.PersonalCode,
                            c.Teacher.FirstName,
                            c.Teacher.LastName,
                            c.Teacher.AccountNumber,
                            c.Teacher.InsuranceNumber,
                            c.Teacher.NationalCode,
                            c.Teacher.Grade,
                            c.Teacher.Ranking
                        }
                        : null,


                    SectionInfo = includeSection && c.SectionNavigation != null
                        ? new
                        {
                            c.SectionNavigation.Id,
                            c.SectionNavigation.Section1
                        }
                        : null

                })
                .FirstOrDefaultAsync();


            if (classResult == null)
                return NotFound();


            return Ok(classResult);
        }
        //==========================
        //PATCH CODES//
        //==========================

//        [HttpPatch("{CourseId}")]
//        public async Task<IActionResult> UpdateClass(
//int CourseId,
//ClassChangeRequest request)
//        {
//            await using var transaction =
//                await _context.Database.BeginTransactionAsync();

//            try
//            {
//                // Find the existing teacher
//                var teacher = await _context.Classes
//                    .FirstOrDefaultAsync(t => t.CourseId == CourseId);

//                if (teacher == null)
//                {
//                    return NotFound("Teacher not found.");
//                }

//                // -------------------------------------------------
//                // 1. Save the CURRENT version into TeachersHistory
//                // -------------------------------------------------

//                var historyRecords = await _context.ClassesHistories
//                    .Where(h => h.CourseId == CourseId)
//                    .OrderBy(h => h.UpdateFlag)
//                    .ToListAsync();

//                // If there are already 3 history records,
//                // delete the oldest one and shift the others.
//                if (historyRecords.Count >= 3)
//                {
//                    var oldestRecord = historyRecords.First();

//                    _context.ClassesHistories.Remove(oldestRecord);

//                    foreach (var record in historyRecords
//                        .Where(h => h.HistoryId != oldestRecord.HistoryId)
//                        .OrderBy(h => h.UpdateFlag))
//                    {
//                        record.UpdateFlag--;
//                    }
//                }

//                // Determine the new history flag
//                var remainingHistoryCount = historyRecords.Count >= 3
//                    ? 2
//                    : historyRecords.Count;

//                int newUpdateFlag = remainingHistoryCount + 1;

//                // Create history from the CURRENT teacher
//                var history = new ClassesHistory
//                {
//                    PersonalCode = teacher.PersonalCode,

//                    FirstName = teacher.FirstName,
//                    LastName = teacher.LastName,
//                    Ranking = teacher.Ranking,
//                    Grade = teacher.Grade,
//                    AccountNumber = teacher.AccountNumber,
//                    InsuranceNumber = teacher.InsuranceNumber,
//                    NationalCode = teacher.NationalCode,

//                    UpdateFlag = newUpdateFlag,
//                    CreationDate = DateOnly.FromDateTime(DateTime.Now)
//                };

//                _context.TeachersHistories.Add(history);

//                // -------------------------------------------------
//                // 2. Modify ONLY the fields supplied by the client
//                // -------------------------------------------------

//                if (request.FirstName != null)
//                    teacher.FirstName = request.FirstName;

//                if (request.LastName != null)
//                    teacher.LastName = request.LastName;

//                if (request.Ranking.HasValue)
//                    teacher.Ranking = request.Ranking;

//                if (request.Grade.HasValue)
//                    teacher.Grade = request.Grade;

//                if (request.AccountNumber != null)
//                    teacher.AccountNumber = request.AccountNumber;

//                if (request.InsuranceNumber != null)
//                    teacher.InsuranceNumber = request.InsuranceNumber;

//                if (request.NationalCode != null)
//                    teacher.NationalCode = request.NationalCode;

//                // PersonalCode is NEVER modified.
//                // Id is NEVER modified.

//                // Save history + teacher update
//                await _context.SaveChangesAsync();

//                // Commit transaction
//                await transaction.CommitAsync();

//                return Ok(new
//                {
//                    message = "Teacher updated successfully.",
//                    personalCode = teacher.PersonalCode,
//                    historyUpdateFlag = history.UpdateFlag
//                });
//            }
//            catch (Exception ex)
//            {
//                await transaction.RollbackAsync();

//                return StatusCode(500, new
//                {
//                    message = "An error occurred while updating the teacher.",
//                    error = ex.Message
//                });
//            }
//        }
    }
}
