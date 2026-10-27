using ExclelToSql.Data;
using ExclelToSql.Models;
using ExclelToSql.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public CoursesApiController(ExclelToSqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await _context.Courses.ToListAsync();

        //    return Ok(data);
        //}
        public async Task<IActionResult> GetCourses(
        int page = 1,
        int pageSize = 50)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 50;

            pageSize = Math.Min(pageSize, 100);

            // Build the query without fetching all records.
            IQueryable<Course> query = _context.Courses.AsNoTracking();

            // Count records before pagination.
            int totalRecords = await query.CountAsync();

            // Fetch only the requested page.
            var courses = await query
                .OrderBy(c => c.CourseId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(
                    (double)totalRecords / pageSize),
                Data = courses
            });
        }
        [HttpGet("{CourseId}")]
        public async Task<IActionResult> GetById(int CourseId)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == CourseId);

            if (course == null)
                return NotFound();

            return Ok(course);
        }
        [HttpPatch("{CourseId}")]
        public async Task<IActionResult> UpdateTeacher(int CourseId, CourseChangeRequest request)
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
                // Find the existing teacher
                // -------------------------------------------------

                var course = await _context.Courses
                    .FirstOrDefaultAsync(t => t.CourseId == CourseId);

                if (course == null)
                {
                    return NotFound("Teacher not found.");
                }

                // -------------------------------------------------
                // 1. Save the CURRENT version into TeachersHistory
                // -------------------------------------------------

                var historyRecords = await _context.CoursesHistories
                    .Where(h => h.CourseId == CourseId)
                    .OrderBy(h => h.UpdateFlag)
                    .ToListAsync();

                // If there are already 3 history records,
                // delete the oldest one and shift the others.
                if (historyRecords.Count >= 3)
                {
                    var oldestRecord = historyRecords.First();

                    _context.CoursesHistories.Remove(oldestRecord);

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
                // Create history from the CURRENT teacher
                // -------------------------------------------------

                var history = new CoursesHistory
                {
                    CourseId = course.CourseId,

                    CourseName = course.CourseName,
                    Theoretical = course.Theoretical,
                    Practical = course.Practical,
                    CreditsCoefficients = course.CreditsCoefficients,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now),
                    UpdateFlag = newUpdateFlag


                    // The logged-in user who made the change
                    //EditorName = editorId
                };

                _context.CoursesHistories.Add(history);

                // -------------------------------------------------
                // 2. Modify ONLY the fields supplied by the client
                // -------------------------------------------------

                if (request.CreditsCoefficients.HasValue)
                    course.CreditsCoefficients = request.CreditsCoefficients;


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
                    message = "Course updated successfully.",
                    CourseId = course.CourseId,
                    historyUpdateFlag = history.UpdateFlag,
                    editorId = history.EditorName
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "An error occurred while updating the course.",
                    error = ex.Message
                });
            }
        }
    }

}