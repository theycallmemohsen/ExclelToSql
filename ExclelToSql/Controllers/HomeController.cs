using ExcelDataReader;
using ExclelToSql.Data;
using ExclelToSql.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ExclelToSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExclelToSqlContext _context;

        public HomeController(ILogger<HomeController> logger , ExclelToSqlContext context)
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UploadExcel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>UploadExcel(IFormFile file)
        {
            
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (file != null && file.Length > 0)
                {
                    var UploadFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";
                    if (!Directory.Exists(UploadFolder))
                    {
                        Directory.CreateDirectory(UploadFolder);
                    }
                    var filePath = Path.Combine(UploadFolder, file.FileName);
                    using (var Stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(Stream);
                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                        // Choose one of either 1 or 2:

                        // 1. Use the reader methods
                        //bool IsHeaderSkipped = false;
                        reader.Read(); // Skip row 1
                        reader.Read(); // Skip row 2

                        do
                        {
                            //bool isHeaderRow = true;
                            while (reader.Read())
                                {
                                //if (!IsHeaderSkipped)
                                //{
                                //    IsHeaderSkipped = true;
                                //    continue;
                                //}
                                //if (isHeaderRow)
                                //{
                                //    isHeaderRow = false;
                                //    continue;
                                //}
                                Excel C = new Excel();

                                C.RowNumber = int.TryParse(reader.GetValue(0)?.ToString(), out int rowNumber) ? rowNumber : null;
                                C.NationalNumber = reader.GetValue(1)?.ToString();
                                C.PersonalCode = reader.GetValue(2)?.ToString();
                                C.FirstName = reader.GetValue(3)?.ToString();
                                C.LastName = reader.GetValue(4)?.ToString();
                                C.Ranking = reader.GetValue(5)?.ToString();
                                C.Grade = int.TryParse(reader.GetValue(6)?.ToString(), out int grade) ? grade : null;
                                C.CourseName = reader.GetValue(7)?.ToString();
                                C.Theoretical = decimal.TryParse(reader.GetValue(8)?.ToString(), out decimal theoretical) ? theoretical : null;
                                C.Practical = decimal.TryParse(reader.GetValue(9)?.ToString(), out decimal practical) ? practical : null;
                                C.Section = reader.GetValue(10)?.ToString();
                                C.StudentNumber = int.TryParse(reader.GetValue(11)?.ToString(), out int studentNumber) ? studentNumber : null;
                                C.CreditsCoefficients = decimal.TryParse(reader.GetValue(12)?.ToString(), out decimal creditsCoefficients) ? creditsCoefficients : null;
                                C.CreditsCoefficientsSum = double.TryParse(reader.GetValue(13)?.ToString(), out double creditsCoefficientsSum) ? creditsCoefficientsSum : null;
                                C.Duty = int.TryParse(reader.GetValue(14)?.ToString(), out int duty) ? duty : null;
                                C.TeachingExtra = double.TryParse(reader.GetValue(15)?.ToString(), out double teachingExtra) ? teachingExtra : null;
                                C.TheoreticalAmount =decimal.TryParse(reader.GetValue(16)?.ToString(), out decimal TheoreticalAmount)? TheoreticalAmount: null; 
                                C.GrossAmount =decimal.TryParse(reader.GetValue(17)?.ToString(), out decimal GrossAmount)? GrossAmount: null;
                                C.AccountNumber = reader.GetValue(18)?.ToString();
                                C.InsuranceNumber = reader.GetValue(19)?.ToString();

                                _context.Add(C);


                                    // reader.GetDouble(0);
                                }
                                await _context.SaveChangesAsync();
                            } while (reader.NextResult());
                        ViewBag.Message = "Success";


                        }
                    }
                }
            else
                ViewBag.Message = "Empty";
            return View();  
        }

    }
}
