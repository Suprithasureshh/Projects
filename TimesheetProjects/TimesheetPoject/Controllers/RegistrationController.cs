using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;
using Microsoft.EntityFrameworkCore;

namespace TimesheetPoject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ITimesheetInterface _timesheetInterface;
        private readonly Timesheet_Context _timesheet_Context;
        public RegistrationController(ITimesheetInterface timesheetInterface, Timesheet_Context timesheet_Context)
        {
            _timesheetInterface = timesheetInterface;
            _timesheet_Context = timesheet_Context;
        }

        [HttpPost("Registration")]
        public IActionResult Registration(RegistrationModel regestrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            return Ok(_timesheetInterface.Register(regestrationModel));
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {            
            return Ok(_timesheetInterface.Login(loginModel));
        }

        [HttpPut("Reset-Password")]
        public IActionResult ResetPassword(LoginModel loginModel)
        {           
            return Ok(_timesheetInterface.ResetPassword(loginModel));
        }
    }
}