using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using SHA_QrAttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHA_QrAttendanceSystem.Controllers
{
    public class HomeController : Controller
    {

        QrContext db;
        
        public HomeController(QrContext context)
        {
            db = context;

        }

        public IActionResult Index()
        {
            ViewBag.res = db.Students.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Index(string InputText)
        {
            ViewBag.res = db.Students.ToList();
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = oCodeGenerator.CreateQrCode(InputText, QRCodeGenerator.ECCLevel.Q);
                QRCode qRCode = new QRCode(qRCodeData);
                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:/image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            ViewBag.res = db.Students.ToList();
            return View();
        }

        public IActionResult Manual()
        {
            return View();
        }
        public IActionResult SaveAttendencSheet()
        {
            var stu = db.Students.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("Student Id,Student Name");
            foreach (var st in stu)
            {
                builder.AppendLine($"{st.StuID},{st.Name}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Attend.csv");
            //using(var workbook = new XLWorkbook())
            //{
            //    var worksheet = workbook.Worksheets.Add("stu");
            //    var currentRow = 1;
            //    worksheet.Cell(currentRow, 1).Value = "StudentId";
            //    worksheet.Cell(currentRow, 1).Value = "StudentName";
            //    foreach (var st in stu)
            //    {
            //        currentRow++;
            //        worksheet.Cell(currentRow, 1).Value = st.StuID;
            //        worksheet.Cell(currentRow, 2).Value = st.Name;

            //    }
            //    using(var stram = new MemoryStream())
            //    {
            //        workbook.SaveAs(stram);
            //        var content = stram.ToArray();
            //        return File(content, "application/vnd.openxmlformats-officiedocument.spreadsheetml.sheet", "stu.xlsx");
            //    }

            //}
        }
        [HttpPost]
        public IActionResult SaveStudent(Student model)
        {
            db.Students.Add(model);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
