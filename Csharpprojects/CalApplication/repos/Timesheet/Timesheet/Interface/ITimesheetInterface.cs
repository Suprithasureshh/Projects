using Microsoft.AspNetCore.Mvc;
using Timesheet.Model;

namespace Timesheet.Interface
{
    public interface ITimesheetInterface
    {
        public IActionResult Regester(RegistrationModel regestrationModel);
        public IActionResult Login(LoginModel loginModel);
        public IActionResult ResetPassword(LoginModel loginModel);
    }
}
