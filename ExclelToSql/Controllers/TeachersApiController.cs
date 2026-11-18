using ExclelToSql.Data;
//using ExclelToSql.Filters;
using ExclelToSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Models.DTO;
using Microsoft.Extensions.Caching.Memory;


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
        private readonly IMemoryCache _cache;


        public TeachersApiController(ExclelToSqlContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;

        }

        //// GET: api/TeachersApi
        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] string? expand)
        //{
        //    IQueryable<Teacher> query = _context.Teachers;

        //    bool includeFee = false;
        //    bool includeRanking = false;
        //    bool includeDuty = false;
        //    bool includeSemester = false;

        //    if (!string.IsNullOrWhiteSpace(expand))
        //    {
        //        var expands = expand
        //            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        //            .Select(x => x.ToLower());

        //        includeFee = expands.Contains("fee") || expands.Contains("all");
        //        includeRanking = expands.Contains("ranking") || expands.Contains("all");
        //        includeDuty = expands.Contains("duty") || expands.Contains("all");
        //        includeSemester = expands.Contains("semester") || expands.Contains("all");

        //    }

        //    if (includeFee)
        //        query = query.Include(t => t.GradeNavigation);

        //    if (includeRanking)
        //        query = query.Include(t => t.RankingNavigation);

        //    if (includeDuty)
        //        query = query.Include(t => t.DutyNavigation);

        //    if (includeSemester)
        //        query = query.Include(t => t.SemesterCodeNavigation);

        //    var result = await query
        //        .Select(t => new
        //        {
        //            t.Id,
        //            t.PersonalCode,
        //            t.FirstName,
        //            t.LastName,
        //            t.AccountNumber,
        //            t.InsuranceNumber,
        //            t.NationalCode,
        //            t.Grade,
        //            t.Ranking,
        //            t.Duty,
        //            t.GroupCode,
        //            t.PhoneNumber,

        //            Fee = includeFee && t.GradeNavigation != null
        //                ? new
        //                {
        //                    t.Grade,
        //                    t.GradeNavigation.AssistantMasterSDegree,
        //                    t.GradeNavigation.InstructorEquivalentToAPhD,
        //                    t.GradeNavigation.AssistantProfessor,
        //                    t.GradeNavigation.AssociateProfessor,
        //                    t.GradeNavigation.Professor,

        //                }
        //                : null,

        //            RankingInfo = includeRanking && t.RankingNavigation != null
        //                ? new
        //                {
        //                    t.RankingNavigation.Id,
        //                    t.RankingNavigation.Ranking1
        //                }
        //                : null,
        //            DutyInfo = includeDuty && t.DutyNavigation != null
        //                 ? new
        //                 {
        //                     t.DutyNavigation.Id,
        //                     t.DutyNavigation.DutyGroupId,
        //                     t.DutyNavigation.Title,
        //                     t.DutyNavigation.Value
        //                 }
        //                : null,
        //            SemesterInfo = includeSemester && t.SemesterCodeNavigation != null
        //                 ? new
        //                 {
        //                     t.SemesterCodeNavigation.Id,
        //                     t.SemesterCodeNavigation.SemesterCode,
        //                     t.SemesterCodeNavigation.Title,


        //                 }
        //                : null,

        //        })
        //        .ToListAsync();

        //    return Ok(result);
        //}

        //// GET: api/TeachersApi/14
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id, [FromQuery] string? expand)
        //{
        //    IQueryable<Teacher> query = _context.Teachers;

        //    bool includeFee = false;
        //    bool includeRanking = false;
        //    bool includeDuty = false;
        //    bool includeSemester = false;



        //    if (!string.IsNullOrWhiteSpace(expand))
        //    {
        //        var expands = expand
        //            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        //            .Select(x => x.ToLower());

        //        includeFee = expands.Contains("fee") || expands.Contains("all");
        //        includeRanking = expands.Contains("ranking") || expands.Contains("all");
        //        includeDuty = expands.Contains("duty") || expands.Contains("all");
        //        includeSemester = expands.Contains("semester") || expands.Contains("all");


        //    }

        //    if (includeFee)
        //        query = query.Include(t => t.GradeNavigation);

        //    if (includeRanking)
        //        query = query.Include(t => t.RankingNavigation);

        //    if (includeDuty)
        //        query = query.Include(t => t.DutyNavigation);

        //    var teacher = await query
        //        .Where(t => t.Id == id)
        //        .Select(t => new
        //        {
        //            t.Id,
        //            t.PersonalCode,
        //            t.FirstName,
        //            t.LastName,
        //            t.AccountNumber,
        //            t.InsuranceNumber,
        //            t.NationalCode,
        //            t.Grade,
        //            t.Ranking,
        //            t.Duty,
        //            t.GroupCode,
        //            t.PhoneNumber,



        //            Fee = includeFee && t.GradeNavigation != null
        //                ? new
        //                {
        //                    t.Grade,
        //                    t.GradeNavigation.AssistantMasterSDegree,
        //                    t.GradeNavigation.InstructorEquivalentToAPhD,
        //                    t.GradeNavigation.AssistantProfessor,
        //                    t.GradeNavigation.AssociateProfessor,
        //                    t.GradeNavigation.Professor,
        //                }
        //                : null,

        //            RankingInfo = includeRanking && t.RankingNavigation != null
        //                ? new
        //                {
        //                    t.RankingNavigation.Id,
        //                    t.RankingNavigation.Ranking1
        //                }
        //                : null,
        //            DutyInfo = includeDuty && t.DutyNavigation != null
        //                 ? new
        //                 {
        //                     t.DutyNavigation.Id,
        //                     t.DutyNavigation.DutyGroupId,
        //                     t.DutyNavigation.Title,
        //                     t.DutyNavigation.Value

        //                 }
        //                : null,
        //            SemesterInfo = includeSemester && t.SemesterCodeNavigation != null
        //                 ? new
        //                 {
        //                     t.SemesterCodeNavigation.Id,
        //                     t.SemesterCodeNavigation.SemesterCode,
        //                     t.SemesterCodeNavigation.Title,


        //                 }
        //                : null,
        //        })
        //        .FirstOrDefaultAsync();

        //    if (teacher == null)
        //        return NotFound();

        //    return Ok(teacher);
        //}
        //NEW CODES??
        public record MajorDto(int Id, string? Major1);
        public record FacultyDto(int? Id, string? Title, List<MajorDto> Majors);
        public record GroupDto(int Id, string? GroupName, List<FacultyDto> Faculties);
        public record PagedResult<T>(List<T> Data, int TotalCount, int Page, int PageSize);

        // add IMemoryCache to your existing constructor:  private readonly IMemoryCache _cache;

        private async Task<Dictionary<int, GroupDto>> GetGroupsAsync()
        {
            var cached = await _cache.GetOrCreateAsync("groups-with-majors", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                var groups = await _context.EducationalGroups.AsNoTracking()
                    .Select(g => new { g.Id, g.GroupName }).ToListAsync();

                var faculties = await _context.Faculties.AsNoTracking()
                    .Select(f => new { f.Id, f.Title }).ToListAsync();
                var majors = await _context.Majors.AsNoTracking()
                    .Select(m => new { m.Id, m.Major1, GroupId = (int?)m.GroupId, FacultyId = (int?)m.FacultyId })
                    .ToListAsync();

                var facultyTitle = faculties.ToDictionary(f => f.Id, f => f.Title);
                var majorsByGroup = majors.ToLookup(m => m.GroupId);

                return groups.ToDictionary(
                    g => g.Id,
                    g => new GroupDto(
                        g.Id, g.GroupName,
                        majorsByGroup[g.Id]
                            .GroupBy(m => m.FacultyId)
                            .Select(fg => new FacultyDto(
                                fg.Key,
                                fg.Key.HasValue ? facultyTitle.GetValueOrDefault(fg.Key.Value) : null,
                                fg.Select(m => new MajorDto(m.Id, m.Major1)).ToList()))
                            .ToList()));
            });
            return cached!;
        }

        private async Task<PagedResult<object>> LoadTeachersAsync(
            int? id,
            string? expand,
            string? search,
            int page,
            int pageSize)
        {
            var ex = (expand ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => x.ToLower()).ToList();

            bool all = ex.Contains("all");
            bool includeFee = all || ex.Contains("fee");
            bool includeRanking = all || ex.Contains("ranking");
            bool includeDuty = all || ex.Contains("duty");
            bool includeSemester = all || ex.Contains("semester");
            bool includeGroup = all || ex.Contains("group");
            bool includeMajors = all || ex.Contains("majors");

            IQueryable<Teacher> query = _context.Teachers.AsNoTracking();

            if (id.HasValue)
                query = query.Where(t => t.Id == id.Value);

            // --- SEARCH LOGIC ---
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim().ToLower();
                query = query.Where(t =>
                    (t.FirstName != null && t.FirstName.ToLower().Contains(searchTerm)) ||
                    (t.LastName != null && t.LastName.ToLower().Contains(searchTerm)) ||
                    (t.AccountNumber != null && t.AccountNumber.ToLower().Contains(searchTerm)) ||
                    (t.PersonalCode != null && t.PersonalCode.ToLower().Contains(searchTerm)) ||
                    (t.NationalCode != null && t.NationalCode.ToLower().Contains(searchTerm)) ||
                    (t.Grade.HasValue && t.Grade.Value.ToString().Contains(searchTerm)) ||
                    (t.Ranking.HasValue && t.Ranking.Value.ToString().Contains(searchTerm)) ||
                    (t.GroupCode.HasValue && t.GroupCode.Value.ToString().Contains(searchTerm))
                );
            }

            // --- PAGINATION LOGIC ---
            var totalCount = await query.CountAsync();

            // Apply pagination BEFORE selecting to optimize SQL query
            var pagedQuery = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var teachers = await pagedQuery.Select(t => new
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

                Fee = includeFee && t.GradeNavigation != null
                    ? new
                    {
                        t.Grade,
                        t.GradeNavigation.AssistantMasterSDegree,
                        t.GradeNavigation.InstructorEquivalentToAPhD,
                        t.GradeNavigation.AssistantProfessor,
                        t.GradeNavigation.AssociateProfessor,
                        t.GradeNavigation.Professor,
                    }
                    : null,

                RankingInfo = includeRanking && t.RankingNavigation != null
                    ? new { t.RankingNavigation.Id, t.RankingNavigation.Ranking1 }
                    : null,

                DutyInfo = includeDuty && t.DutyNavigation != null
                    ? new { t.DutyNavigation.Id, t.DutyNavigation.DutyGroupId, t.DutyNavigation.Title, t.DutyNavigation.Value }
                    : null,

                SemesterInfo = includeSemester && t.SemesterCodeNavigation != null
                    ? new { t.SemesterCodeNavigation.Id, t.SemesterCodeNavigation.SemesterCode, t.SemesterCodeNavigation.Title }
                    : null
            }).ToListAsync();

            var groups = (includeGroup || includeMajors) ? await GetGroupsAsync() : null;

            var resultList = teachers.Select(t =>
            {
                GroupDto? g = null;
                if (groups != null && t.GroupCode != null)
                    groups.TryGetValue(t.GroupCode.Value, out g);

                return (object)new
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
                    t.Fee,
                    t.RankingInfo,
                    t.DutyInfo,
                    t.SemesterInfo,
                    Group = g == null ? null : new { g.Id, g.GroupName, Faculties = includeMajors ? g.Faculties : null }
                };
            }).ToList();

            return new PagedResult<object>(resultList, totalCount, page, pageSize);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? expand,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            // Prevent excessively large page sizes
            if (pageSize > 100) pageSize = 100;
            if (page < 1) page = 1;

            var result = await LoadTeachersAsync(null, expand, search, page, pageSize);
            return Ok(result);
        }

        //// GET: api/TeachersApi/14
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string? expand)
        {
            // We don't need pagination for GetById, so we pass page=1, pageSize=1
            var result = await LoadTeachersAsync(id, expand, null, 1, 1);

            return result.Data.Count == 0 ? NotFound() : Ok(result.Data[0]);
        }

        //To search: GET /api/TeachersApi? search = محمد & page = 1 & pageSize = 50

        //To go to page 2: GET /api/TeachersApi? page = 2 & pageSize = 50

        //To combine: GET /api/TeachersApi? search = محمد & expand = fee & page = 2 & pageSize = 5


        [HttpPatch("{personalCode}")]
        public async Task<IActionResult> UpdateTeacher(string personalCode, TeacherChangeRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {

                // -------------------------------------------------
                // 0. Get the logged-in user's username from Session
                // -------------------------------------------------

                var editorId = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(editorId))
                {
                    return Unauthorized("You must be logged in to edit a teacher.");
                }



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
                    Duty = teacher.Duty,
                    PhoneNumber = teacher.PhoneNumber,
                    CooperationType = teacher.CooperationType,
                    SemesterCode = teacher.SemesterCode,
                    GroupCode = teacher.GroupCode,
                    EditorName = editorId



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

                if (request.CooperationType.HasValue)
                    teacher.CooperationType = request.Duty;

                if (request.GroupCode.HasValue)
                    teacher.GroupCode = request.GroupCode;

                if (request.SemesterCode.HasValue)
                    teacher.SemesterCode = request.SemesterCode;

                if (request.PhoneNumber != null)
                    teacher.PhoneNumber = request.PhoneNumber;


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

        //PUT CODES

        // PUT: api/TeachersApi/{personalCode}
        [HttpPut("{personalCode}")]
        public async Task<IActionResult> CreateTeacher(string personalCode, [FromBody] TeacherChangeRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Ensure the URL PersonalCode matches the body PersonalCode
                if (personalCode != request.PersonalCode)
                    return BadRequest("The PersonalCode in the URL must match the PersonalCode in the body.");

                // Check if the teacher already exists
                var exists = await _context.Teachers.AnyAsync(t => t.PersonalCode == personalCode);
                if (exists)
                    return Conflict($"A teacher with PersonalCode {personalCode} already exists.");

                // Map DTO to Entity
                var teacher = new Teacher
                {
                    PersonalCode = request.PersonalCode,
                    NationalCode = request.NationalCode,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Ranking = request.Ranking,
                    Grade = request.Grade,
                    AccountNumber = request.AccountNumber,
                    InsuranceNumber = request.InsuranceNumber,
                    Duty = request.Duty,
                    GroupCode = request.GroupCode,
                    SemesterCode = request.SemesterCode,
                    CooperationType = request.CooperationType,
                    PhoneNumber = request.PhoneNumber,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now)
                };

                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Teacher created successfully.",
                    personalCode = teacher.PersonalCode
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "An error occurred while creating the teacher.",
                    error = ex.Message
                });
            }
        }

        //DELETE CODES

        // DELETE: api/TeachersApi/{personalCode}
        [HttpDelete("{personalCode}")]
        public async Task<IActionResult> DeleteTeacher(string personalCode)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.PersonalCode == personalCode);

                if (teacher == null)
                {
                    return NotFound("Teacher not found.");
                }

                // 1. Delete related history records first to avoid FK constraints
                var historyRecords = await _context.TeachersHistories
                    .Where(h => h.PersonalCode == personalCode)
                    .ToListAsync();

                if (historyRecords.Any())
                {
                    _context.TeachersHistories.RemoveRange(historyRecords);
                }

                // 2. Delete the teacher
                _context.Teachers.Remove(teacher);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Teacher deleted successfully.",
                    personalCode = personalCode
                });
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new
                {
                    message = "Cannot delete this teacher because they are referenced by other tables (e.g., Classes). Please remove those references first.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the teacher.",
                    error = ex.Message
                });
            }
        }
    }





}