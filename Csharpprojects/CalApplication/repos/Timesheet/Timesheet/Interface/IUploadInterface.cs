using Microsoft.AspNetCore.Mvc;
using Timesheet.Model;

namespace Timesheet.Interface
{
    public interface IUploadInterface
    {
        public IActionResult add(UploadModel[] entries, int selectedMonth);
        public IActionResult add1(EmployeeModel[] entries);
        List<UploadModel> GetTSDet(int userId);
        List<EmployeeModel> GetEmpDet(int userId);
    }
}
