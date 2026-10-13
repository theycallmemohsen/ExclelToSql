//using ExcelDataReader;
//using ExclelToSql.Data;
//using ExclelToSql.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using System.Text;

//namespace ExclelToSql.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly ExclelToSqlContext _context;

//        public HomeController(ILogger<HomeController> logger, ExclelToSqlContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//        public IActionResult UploadExcel()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> UploadExcel(IFormFile file)
//        {

//            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//            if (file != null && file.Length > 0)
//            {
//                var UploadFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";
//                if (!Directory.Exists(UploadFolder))
//                {
//                    Directory.CreateDirectory(UploadFolder);
//                }
//                var filePath = Path.Combine(UploadFolder, file.FileName);
//                using (var Stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await file.CopyToAsync(Stream);
//                }
//                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
//                {
//                    // Auto-detect format, supports:
//                    //  - Binary Excel files (2.0-2003 format; *.xls)
//                    //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
//                    using (var reader = ExcelReaderFactory.CreateReader(stream))
//                    {
//                        // Choose one of either 1 or 2:

//                        // 1. Use the reader methods
//                        //bool IsHeaderSkipped = false;
//                        reader.Read(); // Skip row 1
//                        //reader.Read(); // Skip row 2

//                        do
//                        {
//                            //bool isHeaderRow = true;
//                            while (reader.Read())
//                            {
//                                //if (!IsHeaderSkipped)
//                                //{
//                                //    IsHeaderSkipped = true;
//                                //    continue;
//                                //}
//                                //if (isHeaderRow)
//                                //{
//                                //    isHeaderRow = false;
//                                //    continue;
//                                //}
//                                EducationalGroup C = new EducationalGroup();

//                                C.GroupName = reader.GetValue(1)?.ToString()?.Trim() ?? "";






//                                _context.Add(C);


//                                // reader.GetDouble(0);
//                            }
//                            await _context.SaveChangesAsync();
//                        } while (reader.NextResult());
//                        ViewBag.Message = "Success";


//                    }
//                }
//            }
//            else
//                ViewBag.Message = "Empty";
//            return View();
//        }

//    }
//}




using ExcelDataReader;
using ExclelToSql.Data;
using ExclelToSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace ExclelToSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExclelToSqlContext _context;

        public HomeController(ILogger<HomeController> logger, ExclelToSqlContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "No file was received. Please select an Excel file.";
                return View();
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file != null && file.Length > 0)
            {
                try
                {
                    var uploadFolder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Uploads");

                    Directory.CreateDirectory(uploadFolder);

                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(uploadFolder, fileName);

                    await using (var outputStream = new FileStream(
                        filePath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(outputStream);
                    }

                    int inserted = 0;
                    int skipped = 0;

                    // Prevent duplicate PersonalCode values within this upload.
                    var importedCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    await using (var stream = new FileStream(
                        filePath, FileMode.Open, FileAccess.Read))
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool skipFirstRow = true;

                        do
                        {
                            while (reader.Read())
                            {
                                if (skipFirstRow)
                                {
                                    skipFirstRow = false;
                                    continue;
                                }

                                //string coursename =
                                //    reader.GetValue(1)?.ToString()?.Trim() ?? "";

                                //if (string.IsNullOrWhiteSpace(coursename))
                                //{
                                //    skipped++;
                                //    continue;
                                //}

                                //if (!importedCodes.Add(coursename))
                                //{
                                //    skipped++;
                                //    continue;
                                //}

                                //bool alreadyExists = await _context.Courses
                                //    .AnyAsync(t => t.CourseName == coursename);

                                //if (alreadyExists)
                                //{
                                //    skipped++;
                                //    continue;
                                //}


                                OtherTeacher C = new OtherTeacher();
                                //===============================================================================
                                string value = reader.GetValue(0)?.ToString()?.Trim() ?? "";

                                value = value
                                    .Replace('۰', '0').Replace('۱', '1')
                                    .Replace('۲', '2').Replace('۳', '3')
                                    .Replace('۴', '4').Replace('۵', '5')
                                    .Replace('۶', '6').Replace('۷', '7')
                                    .Replace('۸', '8').Replace('۹', '9')
                                    .Replace('٠', '0').Replace('١', '1')
                                    .Replace('٢', '2').Replace('٣', '3')
                                    .Replace('٤', '4').Replace('٥', '5')
                                    .Replace('٦', '6').Replace('٧', '7')
                                    .Replace('٨', '8').Replace('٩', '9');

                                C.PresentationCode = string.IsNullOrWhiteSpace(value) ? null : value;

                                string value2 = reader.GetValue(1)?.ToString()?.Trim() ?? "";

                                value2 = value2
                                    .Replace('۰', '0').Replace('۱', '1')
                                    .Replace('۲', '2').Replace('۳', '3')
                                    .Replace('۴', '4').Replace('۵', '5')
                                    .Replace('۶', '6').Replace('۷', '7')
                                    .Replace('۸', '8').Replace('۹', '9')
                                    .Replace('٠', '0').Replace('١', '1')
                                    .Replace('٢', '2').Replace('٣', '3')
                                    .Replace('٤', '4').Replace('٥', '5')
                                    .Replace('٦', '6').Replace('٧', '7')
                                    .Replace('٨', '8').Replace('٩', '9');

                                C.TeacherId = string.IsNullOrWhiteSpace(value2) ? null : value2;

                                string value3 = reader.GetValue(2)?.ToString()?.Trim() ?? "";

                                C.TeacherFullName = string.IsNullOrWhiteSpace(value3) ? null : value3;

                                string value4 = reader.GetValue(3)?.ToString()?.Trim() ?? "";

                                C.Role = string.IsNullOrWhiteSpace(value4) ? null : value4;

                                _context.OtherTeachers.Add(C);
                                inserted++;
                                //================================================================
                            }
                        }
                        while (reader.NextResult());

                        await _context.SaveChangesAsync();
                    }

                    ViewBag.Message =
                        $"Import completed. Appended: {inserted}; skipped: {skipped}.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error importing the Excel file.");

                    var details = new StringBuilder();
                    details.AppendLine(ex.Message);

                    var inner = ex.InnerException;
                    while (inner != null)
                    {
                        details.AppendLine(inner.Message);
                        inner = inner.InnerException;
                    }

                    ViewBag.Message = $"Import failed: {details}";
                }
            }
            else
            {
                ViewBag.Message = "Empty";
            }

            return View();
        }
    }
}
















