using Microsoft.AspNetCore.Mvc;
using TimesheetPoject.Model;

namespace TimesheetPoject.Interface
{
    public interface IUploadInterface
    {
        public IActionResult add(UploadModel[] entries);
        public IActionResult add1(EmployeeModel[] entries);
        List<UploadModel> GetTSDet();
        List<EmployeeModel> GetEmpDet();
    }
}
