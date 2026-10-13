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
                query = query.Include(c => c.NationalCodeNavigation);

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
                    c.SessionsCount,


                    Course = includeCourse && c.Course != null
                        ? new
                        {
                            
                            c.Course.CourseId,
                            c.Course.CourseName,
                            c.Course.Theoretical,
                            c.Course.Practical,
                            c.Course.CreditsCoefficients
                        }
                        : null,


                    Teacher = includeTeacher && c.NationalCodeNavigation != null
                        ? new
                        {
                            c.NationalCodeNavigation.Id,
                            c.NationalCodeNavigation.PersonalCode,
                            c.NationalCodeNavigation.FirstName,
                            c.NationalCodeNavigation.LastName,
                            c.NationalCodeNavigation.AccountNumber,
                            c.NationalCodeNavigation.InsuranceNumber,
                            c.NationalCodeNavigation.NationalCode,
                            c.NationalCodeNavigation.Grade,
                            c.NationalCodeNavigation.Ranking
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
                query = query.Include(c => c.NationalCodeNavigation);

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

                            c.Course.CourseId,
                            c.Course.CourseName,
                            c.Course.Theoretical,
                            c.Course.Practical,
                            c.Course.CreditsCoefficients
                        }
                        : null,


                    Teacher = includeTeacher && c.NationalCodeNavigation != null
                        ? new
                        {
                            c.NationalCodeNavigation.Id,
                            c.NationalCodeNavigation.PersonalCode,
                            c.NationalCodeNavigation.FirstName,
                            c.NationalCodeNavigation.LastName,
                            c.NationalCodeNavigation.AccountNumber,
                            c.NationalCodeNavigation.InsuranceNumber,
                            c.NationalCodeNavigation.NationalCode,
                            c.NationalCodeNavigation.Grade,
                            c.NationalCodeNavigation.Ranking
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
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateClass(
        int Id,
        ClassChangeRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // -------------------------------------------------
                // 0. Get the logged-in user's username from Session
                // -------------------------------------------------

                //var editorId = HttpContext.Session.GetString("Username");

                //if (string.IsNullOrEmpty(editorId))
                //{
                //    return Unauthorized("You must be logged in to edit a teacher.");
                //}

                // -------------------------------------------------
                // Find the existing class
                // -------------------------------------------------

                var Class = await _context.Classes
                    .FirstOrDefaultAsync(t => t.Id == Id);

                if (Class == null)
                {
                    return NotFound("Class not found.");
                }

                // -------------------------------------------------
                // 1. Save the CURRENT version into ClassesHistory
                // -------------------------------------------------

                var historyRecords = await _context.ClassesHistories
                    .Where(h => h.Id == Id)
                    .OrderBy(h => h.UpdateFlag)
                    .ToListAsync();

                // If there are already 3 history records,
                // delete the oldest one and shift the others.
                if (historyRecords.Count >= 3)
                {
                    var oldestRecord = historyRecords.First();

                    _context.ClassesHistories.Remove(oldestRecord);

                    foreach (var record in historyRecords
                        .Where(h => h.HistoryId != oldestRecord.HistoryId)
                        .OrderBy(h => h.UpdateFlag))
                    {
                        record.UpdateFlag--;
                    }
                }

                // Determine the new history flag
                var remainingHistoryCount = historyRecords.Count >= 3
                    ? 2
                    : historyRecords.Count;

                int newUpdateFlag = remainingHistoryCount + 1;

                // -------------------------------------------------
                // Create history from the CURRENT class
                // -------------------------------------------------

                var history = new ClassesHistory
                {
                    Id = Class.Id,
                    CourseId = Class.CourseId,
                    TeacherId = Class.TeacherId,
                    Section = Class.Section,
                    StudentsCount = Class.StudentsCount,
                    SessionsCount = Class.SessionsCount,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now),
                    UpdateFlag = newUpdateFlag

                    // The logged-in user who made the change
                    //EditorName = editorId
                };

                _context.ClassesHistories.Add(history);

                // -------------------------------------------------
                // 2. Modify ONLY the fields supplied by the client
                // -------------------------------------------------

                if (request.Section.HasValue)
                    Class.Section = request.Section;

                if (request.StudentsCount.HasValue)
                    Class.StudentsCount = request.StudentsCount;

                if (request.SessionsCount.HasValue)
                    Class.SessionsCount = request.SessionsCount;


                // PersonalCode is NEVER modified.
                // Id is NEVER modified.

                // -------------------------------------------------
                // 3. Save history + teacher update
                // -------------------------------------------------

                await _context.SaveChangesAsync();

                // -------------------------------------------------
                // 4. Commit transaction
                // -------------------------------------------------

                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Class updated successfully.",
                    Id = Class.Id,
                    historyUpdateFlag = history.UpdateFlag,
                    editorId = history.EditorName
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    exception = ex.GetType().FullName,
                    message = ex.Message,
                    innerException = ex.InnerException?.GetType().FullName,
                    innerMessage = ex.InnerException?.Message,
                    fullException = ex.ToString()
                });
            }
        }
    }
}
