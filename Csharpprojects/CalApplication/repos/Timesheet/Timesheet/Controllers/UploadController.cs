using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using Timesheet.Interface;
using Timesheet.Model;

namespace Timesheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadInterface _Iupload;

        public UploadController(IUploadInterface iupload)
        {
            _Iupload = iupload;
        }

        [HttpPost("data")]
        public IActionResult Post(UploadModel[] entries)
        {
            return Ok(_Iupload.add(entries));
        }

        [HttpPost("info")]
        public IActionResult Post1(EmployeeModel[] entries)
        {
            return Ok(_Iupload.add1(entries));
        }
        [HttpGet("Emp Details")]
        public List<EmployeeModel> select(int userId)
        {
            return _Iupload.GetEmpDet(userId).ToList();
        }
        [HttpGet("TimeSheet Details")]
        public List<UploadModel> select1(int userId)
        {
            return _Iupload.GetTSDet(userId).ToList();
        }
        [HttpGet("To Export the data")]
        public IActionResult ExportTimeSheets(int userId)
        {
            var timesheet = _Iupload.GetTSDet(userId);
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(timesheet);
                var content = writer.ToString();
                var bytes = Encoding.UTF8.GetBytes(content); var result = new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "TimeSheet.csv"
                }; return result;
            }
        }
    }
}
