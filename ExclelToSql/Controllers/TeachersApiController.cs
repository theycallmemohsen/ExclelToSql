
using ExclelToSql.Data;
using ExclelToSql.Filters;
using ExclelToSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Models.DTO;

namespace ExclelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //-------------------
    //[SessionAuthorize]
    //--------------------
    public class TeachersApiController : ControllerBase
    {
        private readonly ExclelToSqlContext _context;

        public TeachersApiController(ExclelToSqlContext context)
        {
            _context = context;
        }

        // GET: api/TeachersApi
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? expand)
        {
            IQueryable<Teacher> query = _context.Teachers;

            bool includeFee = false;
            bool includeRanking = false;

            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeFee = expands.Contains("fee") || expands.Contains("all");
                includeRanking = expands.Contains("ranking") || expands.Contains("all");
            }

            if (includeFee)
                query = query.Include(t => t.GradeNavigation);

            if (includeRanking)
                query = query.Include(t => t.RankingNavigation);

            var result = await query
                .Select(t => new
                {
                    t.Id,
                    t.PersonalCode,
                    t.FirstName,
                    t.LastName,
                    t.AccountNumber,
                    t.InsuranceNumber,
                    t.NationalCode,
                    t.Grade,
                    t.Ranking,

                    Fee = includeFee && t.GradeNavigation != null
                        ? new
                        {
                            t.GradeNavigation.Grade,
                            t.GradeNavigation.BachelorTeacher,
                            t.GradeNavigation.MastersTeacher,
                            t.GradeNavigation.MastersTrainer,
                            t.GradeNavigation.Phdtrainer,
                            t.GradeNavigation.AssistantTeacher,
                            t.GradeNavigation.AssociateTeacher,
                            t.GradeNavigation.Teacher
                        }
                        : null,

                    RankingInfo = includeRanking && t.RankingNavigation != null
                        ? new
                        {
                            t.RankingNavigation.Id,
                            t.RankingNavigation.Ranking1
                        }
                        : null
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/TeachersApi/14
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string? expand)
        {
            IQueryable<Teacher> query = _context.Teachers;

            bool includeFee = false;
            bool includeRanking = false;

            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeFee = expands.Contains("fee") || expands.Contains("all");
                includeRanking = expands.Contains("ranking") || expands.Contains("all");
            }

            if (includeFee)
                query = query.Include(t => t.GradeNavigation);

            if (includeRanking)
                query = query.Include(t => t.RankingNavigation);

            var teacher = await query
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Id,
                    t.PersonalCode,
                    t.FirstName,
                    t.LastName,
                    t.AccountNumber,
                    t.InsuranceNumber,
                    t.NationalCode,
                    t.Grade,
                    t.Ranking,

                    Fee = includeFee && t.GradeNavigation != null
                        ? new
                        {
                            t.GradeNavigation.Grade,
                            t.GradeNavigation.BachelorTeacher,
                            t.GradeNavigation.MastersTeacher,
                            t.GradeNavigation.MastersTrainer,
                            t.GradeNavigation.Phdtrainer,
                            t.GradeNavigation.AssistantTeacher,
                            t.GradeNavigation.AssociateTeacher,
                            t.GradeNavigation.Teacher
                        }
                        : null,

                    RankingInfo = includeRanking && t.RankingNavigation != null
                        ? new
                        {
                            t.RankingNavigation.Id,
                            t.RankingNavigation.Ranking1
                        }
                        : null
                })
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }
        //=========================================
        //PUT CODES
        //=========================================


        //[HttpPut("{personalCode}")]
        //public async Task<IActionResult> UpdateTeacher(string personalCode,TeacherChangeRequest request)
        //{
        //    await using var transaction =
        //        await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        // 1. Find the existing teacher
        //        var teacher = await _context.Teachers
        //            .FirstOrDefaultAsync(t => t.PersonalCode == personalCode);

        //        if (teacher == null)
        //        {
        //            return NotFound("Teacher not found.");
        //        }

        //        // 2. Get existing history records for this teacher
        //        var historyRecords = await _context.TeachersHistory
        //            .Where(h => h.PersonalCode == personalCode)
        //            .OrderBy(h => h.UpdateFlag)
        //            .ToListAsync();

        //        // 3. If already 3 history records exist,
        //        // remove the oldest and shift the remaining flags.
        //        if (historyRecords.Count >= 3)
        //        {
        //            var oldestRecord = historyRecords.First();

        //            _context.TeachersHistory.Remove(oldestRecord);

        //            // Shift 2 -> 1 and 3 -> 2
        //            foreach (var record in historyRecords
        //                .Where(h => h.HistoryId != oldestRecord.HistoryId))
        //            {
        //                record.UpdateFlag--;
        //            }

        //            await _context.SaveChangesAsync();
        //        }

        //        // 4. Determine the UpdateFlag for the new history record
        //        var remainingCount = await _context.TeachersHistory
        //            .CountAsync(h => h.PersonalCode == personalCode);

        //        int newUpdateFlag = remainingCount + 1;

        //        // 5. Create a history record containing the OLD data
        //        var history = new TeacherHistory
        //        {
        //            PersonalCode = teacher.PersonalCode,
        //            FirstName = teacher.FirstName,
        //            LastName = teacher.LastName,
        //            Ranking = teacher.Ranking,
        //            Grade = teacher.Grade,
        //            AccountNumber = teacher.AccountNumber,
        //            InsuranceNumber = teacher.InsuranceNumber,
        //            NationalCode = teacher.NationalCode,

        //            UpdateFlag = newUpdateFlag,
        //            CreationDate = DateOnly.FromDateTime(DateTime.Now)
        //        };

        //        // Add old version to TeachersHistory
        //        _context.TeachersHistory.Add(history);

        //        // 6. Update ONLY the allowed columns in Teachers
        //        teacher.FirstName = request.FirstName;
        //        teacher.LastName = request.LastName;
        //        teacher.Ranking = request.Ranking;
        //        teacher.Grade = request.Grade;
        //        teacher.AccountNumber = request.AccountNumber;
        //        teacher.InsuranceNumber = request.InsuranceNumber;
        //        teacher.NationalCode = request.NationalCode;

        //        // PersonalCode is NEVER changed
        //        // teacher.PersonalCode = ...;  <-- Never do this

        //        // 7. Save both the history and the updated teacher
        //        await _context.SaveChangesAsync();

        //        // 8. Make all changes permanent
        //        await transaction.CommitAsync();

        //        return Ok(new
        //        {
        //            message = "Teacher updated and previous version archived successfully.",

        //            personalCode = teacher.PersonalCode,

        //            historyUpdateFlag = history.UpdateFlag,

        //            teacher = new
        //            {
        //                teacher.Id,
        //                teacher.PersonalCode,
        //                teacher.FirstName,
        //                teacher.LastName,
        //                teacher.Ranking,
        //                teacher.Grade,
        //                teacher.AccountNumber,
        //                teacher.InsuranceNumber,
        //                teacher.NationalCode
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();

        //        return StatusCode(500, new
        //        {
        //            message = "An error occurred while updating the teacher.",
        //            error = ex.Message
        //        });
        //    }
        //}
        //=====================================
        //PATCH CODES
        //=====================================
        [HttpPatch("{personalCode}")]
        public async Task<IActionResult> UpdateTeacher(string personalCode, TeacherChangeRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Find the existing teacher
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.PersonalCode == personalCode);

                if (teacher == null)
                {
                    return NotFound("Teacher not found.");
                }

                // -------------------------------------------------
                // 1. Save the CURRENT version into TeachersHistory
                // -------------------------------------------------

                var historyRecords = await _context.TeachersHistories
                    .Where(h => h.PersonalCode == personalCode)
                    .OrderBy(h => h.UpdateFlag)
                    .ToListAsync();

                // If there are already 3 history records,
                // delete the oldest one and shift the others.
                if (historyRecords.Count >= 3)
                {
                    var oldestRecord = historyRecords.First();

                    _context.TeachersHistories.Remove(oldestRecord);

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

                // Create history from the CURRENT teacher
                var history = new TeachersHistory
                {
                    PersonalCode = teacher.PersonalCode,

                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Ranking = teacher.Ranking,
                    Grade = teacher.Grade,
                    AccountNumber = teacher.AccountNumber,
                    InsuranceNumber = teacher.InsuranceNumber,
                    NationalCode = teacher.NationalCode,

                    UpdateFlag = newUpdateFlag,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now)
                };

                _context.TeachersHistories.Add(history);

                // -------------------------------------------------
                // 2. Modify ONLY the fields supplied by the client
                // -------------------------------------------------

                if (request.FirstName != null)
                    teacher.FirstName = request.FirstName;

                if (request.LastName != null)
                    teacher.LastName = request.LastName;

                if (request.Ranking.HasValue)
                    teacher.Ranking = request.Ranking;

                if (request.Grade.HasValue)
                    teacher.Grade = request.Grade;

                if (request.AccountNumber != null)
                    teacher.AccountNumber = request.AccountNumber;

                if (request.InsuranceNumber != null)
                    teacher.InsuranceNumber = request.InsuranceNumber;

                if (request.NationalCode != null)
                    teacher.NationalCode = request.NationalCode;

                // PersonalCode is NEVER modified.
                // Id is NEVER modified.

                // Save history + teacher update
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Teacher updated successfully.",
                    personalCode = teacher.PersonalCode,
                    historyUpdateFlag = history.UpdateFlag
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "An error occurred while updating the teacher.",
                    error = ex.Message
                });
            }
        }
        //[HttpPatch("{personalCode}")]
        //public async Task<IActionResult> UpdateTeacher(
        //string personalCode,
        //TeacherChangeRequest request)
        //{
        //    await using var transaction =
        //        await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        // -------------------------------------------------
        //        // 0. Get the logged-in user's username from Session
        //        // -------------------------------------------------

        //        var editorId = HttpContext.Session.GetString("Username");

        //        if (string.IsNullOrEmpty(editorId))
        //        {
        //            return Unauthorized("You must be logged in to edit a teacher.");
        //        }

        //        // -------------------------------------------------
        //        // Find the existing teacher
        //        // -------------------------------------------------

        //        var teacher = await _context.Teachers
        //            .FirstOrDefaultAsync(t => t.PersonalCode == personalCode);

        //        if (teacher == null)
        //        {
        //            return NotFound("Teacher not found.");
        //        }

        //        // -------------------------------------------------
        //        // 1. Save the CURRENT version into TeachersHistory
        //        // -------------------------------------------------

        //        var historyRecords = await _context.TeachersHistories
        //            .Where(h => h.PersonalCode == personalCode)
        //            .OrderBy(h => h.UpdateFlag)
        //            .ToListAsync();

        //        // If there are already 3 history records,
        //        // delete the oldest one and shift the others.
        //        if (historyRecords.Count >= 3)
        //        {
        //            var oldestRecord = historyRecords.First();

        //            _context.TeachersHistories.Remove(oldestRecord);

        //            foreach (var record in historyRecords
        //                .Where(h => h.HistoryId != oldestRecord.HistoryId)
        //                .OrderBy(h => h.UpdateFlag))
        //            {
        //                record.UpdateFlag--;
        //            }
        //        }

        //        // Determine the new history flag
        //        var remainingHistoryCount = historyRecords.Count >= 3
        //            ? 2
        //            : historyRecords.Count;

        //        int newUpdateFlag = remainingHistoryCount + 1;

        //        // -------------------------------------------------
        //        // Create history from the CURRENT teacher
        //        // -------------------------------------------------

        //        var history = new TeachersHistory
        //        {
        //            PersonalCode = teacher.PersonalCode,

        //            FirstName = teacher.FirstName,
        //            LastName = teacher.LastName,
        //            Ranking = teacher.Ranking,
        //            Grade = teacher.Grade,
        //            AccountNumber = teacher.AccountNumber,
        //            InsuranceNumber = teacher.InsuranceNumber,
        //            NationalCode = teacher.NationalCode,

        //            UpdateFlag = newUpdateFlag,
        //            CreationDate = DateOnly.FromDateTime(DateTime.Now),

        //            // The logged-in user who made the change
        //            EditorName = editorId
        //        };

        //        _context.TeachersHistories.Add(history);

        //        // -------------------------------------------------
        //        // 2. Modify ONLY the fields supplied by the client
        //        // -------------------------------------------------

        //        if (request.FirstName != null)
        //            teacher.FirstName = request.FirstName;

        //        if (request.LastName != null)
        //            teacher.LastName = request.LastName;

        //        if (request.Ranking.HasValue)
        //            teacher.Ranking = request.Ranking;

        //        if (request.Grade.HasValue)
        //            teacher.Grade = request.Grade;

        //        if (request.AccountNumber != null)
        //            teacher.AccountNumber = request.AccountNumber;

        //        if (request.InsuranceNumber != null)
        //            teacher.InsuranceNumber = request.InsuranceNumber;

        //        if (request.NationalCode != null)
        //            teacher.NationalCode = request.NationalCode;

        //        // PersonalCode is NEVER modified.
        //        // Id is NEVER modified.

        //        // -------------------------------------------------
        //        // 3. Save history + teacher update
        //        // -------------------------------------------------

        //        await _context.SaveChangesAsync();

        //        // -------------------------------------------------
        //        // 4. Commit transaction
        //        // -------------------------------------------------

        //        await transaction.CommitAsync();

        //        return Ok(new
        //        {
        //            message = "Teacher updated successfully.",
        //            personalCode = teacher.PersonalCode,
        //            historyUpdateFlag = history.UpdateFlag,
        //            editorId = history.EditorName
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();

        //        return StatusCode(500, new
        //        {
        //            message = "An error occurred while updating the teacher.",
        //            error = ex.Message
        //        });
        //    }
        //}
    }

}