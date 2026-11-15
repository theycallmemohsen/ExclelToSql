
using ExclelToSql.Data;
//using ExclelToSql.Filters;
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
            bool includeDuty = false;
            bool includeSemester = false;

            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeFee = expands.Contains("fee") || expands.Contains("all");
                includeRanking = expands.Contains("ranking") || expands.Contains("all");
                includeDuty = expands.Contains("duty") || expands.Contains("all");
                includeSemester = expands.Contains("semester") || expands.Contains("all");

            }

            if (includeFee)
                query = query.Include(t => t.Grade1);

            if (includeRanking)
                query = query.Include(t => t.RankingNavigation);

            if (includeDuty)
                query = query.Include(t => t.DutyNavigation);

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
                    t.Duty,
                    t.GroupCode,
                    t.PhoneNumber,

                    Fee = includeFee && t.Grade1 != null
                        ? new
                        {
                            t.Grade,
                            t.Grade1.AssistantMasterSDegree,
                            t.Grade1.InstructorEquivalentToAPhD,
                            t.Grade1.AssistantProfessor,
                            t.Grade1.AssociateProfessor,
                            t.Grade1.Professor,

                        }
                        : null,

                    RankingInfo = includeRanking && t.RankingNavigation != null
                        ? new
                        {
                            t.RankingNavigation.Id,
                            t.RankingNavigation.Ranking1
                        }
                        : null,
                        DutyInfo = includeDuty && t.DutyNavigation != null
                         ? new
                         {
                             t.DutyNavigation.Id,
                             t.DutyNavigation.DutyGroupId,
                             t.DutyNavigation.Title,
                             t.DutyNavigation.Value
                         }
                        : null,
                    SemesterInfo = includeSemester && t.SemesterCodeNavigation != null
                         ? new
                         {
                             t.SemesterCodeNavigation.Id,
                             t.SemesterCodeNavigation.SemesterCode,
                             t.SemesterCodeNavigation.Title,


                         }
                        : null,

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
            bool includeDuty = false;


            if (!string.IsNullOrWhiteSpace(expand))
            {
                var expands = expand
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.ToLower());

                includeFee = expands.Contains("fee") || expands.Contains("all");
                includeRanking = expands.Contains("ranking") || expands.Contains("all");
                includeDuty = expands.Contains("duty") || expands.Contains("all");

            }

            if (includeFee)
                query = query.Include(t => t.Grade1);

            if (includeRanking)
                query = query.Include(t => t.RankingNavigation);

            if (includeDuty)
                query = query.Include(t => t.DutyNavigation);

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
                    t.Duty,
                    t.GroupCode,
                    t.PhoneNumber,



                    Fee = includeFee && t.Grade1 != null
                        ? new
                        {
                            t.Grade,
                            t.Grade1.AssistantMasterSDegree,
                            t.Grade1.InstructorEquivalentToAPhD,
                            t.Grade1.AssistantProfessor,
                            t.Grade1.AssociateProfessor,
                            t.Grade1.Professor,
                        }
                        : null,

                    RankingInfo = includeRanking && t.RankingNavigation != null
                        ? new
                        {
                            t.RankingNavigation.Id,
                            t.RankingNavigation.Ranking1
                        }
                        : null,
                    DutyInfo = includeDuty && t.DutyNavigation != null
                         ? new
                         {
                             t.DutyNavigation.Id,
                             t.DutyNavigation.DutyGroupId,
                             t.DutyNavigation.Title,
                             t.DutyNavigation.Value

                         }
                        : null,
                })
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

    
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
                    CreationDate = DateOnly.FromDateTime(DateTime.Now),
                    Duty = teacher.Duty
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

                if (request.Duty.HasValue)
                    teacher.Duty = request.Duty;


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