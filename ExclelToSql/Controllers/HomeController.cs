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
using System.Globalization;
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


                                TempTeachers2 C = new TempTeachers2();
                                //===============================================================================
                                string value1 = reader.GetValue(0)?.ToString()?.Trim() ?? "";

                                C.TempCooperationType = string.IsNullOrWhiteSpace(value1) ? null : value1;

                                string value2 = reader.GetValue(1)?.ToString()?.Trim() ?? "";

                                C.PersonalCode = string.IsNullOrWhiteSpace(value2) ? null : value2;
                                string value3 = reader.GetValue(2)?.ToString()?.Trim() ?? "";

                                C.FirstName = string.IsNullOrWhiteSpace(value3) ? null : value3;
                                string value4 = reader.GetValue(3)?.ToString()?.Trim() ?? "";

                                C.LastName = string.IsNullOrWhiteSpace(value4) ? null : value4;
                                string value5 = reader.GetValue(4)?.ToString()?.Trim() ?? "";

                                C.TempRanking = string.IsNullOrWhiteSpace(value5) ? null : value5;

                                string value6 = reader.GetValue(5)?.ToString()?.Trim() ?? "";

                                C.Grade = int.TryParse(value6, NumberStyles.Integer, CultureInfo.InvariantCulture, out int grade)
                                    ? grade
                                    : (int?)null;
                                string value7 = reader.GetValue(6)?.ToString()?.Trim() ?? "";

                                C.NationalCode = string.IsNullOrWhiteSpace(value7) ? null : value7;

                                string value8 = reader.GetValue(7)?.ToString()?.Trim() ?? "";

                                C.AccountNumber = string.IsNullOrWhiteSpace(value8) ? null : value8;

                                string value9 = reader.GetValue(8)?.ToString()?.Trim() ?? "";

                                C.InsuranceNumber = string.IsNullOrWhiteSpace(value9) ? null : value9;

                                string value10 = reader.GetValue(9)?.ToString()?.Trim() ?? "";

                                C.PhoneNumber = string.IsNullOrWhiteSpace(value10) ? null : value10;


                                _context.TempTeachers2s.Add(C);
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
















