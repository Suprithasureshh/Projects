using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Timesheet.DataContext;
using Timesheet.Interface;
using Timesheet.Model;

namespace Timesheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegestrationController : ControllerBase
    {
        private readonly ITimesheetInterface _timesheetInterface;
        private readonly Timesheet_Context _timesheet_Context;
        public RegestrationController(ITimesheetInterface timesheetInterface, Timesheet_Context timesheet_Context)
        {
            _timesheetInterface = timesheetInterface;
            _timesheet_Context = timesheet_Context;
        }
        [HttpPost("Registration")]
        public IActionResult Regestration(RegistrationModel regestrationModel)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(regestrationModel.Password);
            regestrationModel.HashKeyPassword = passwordHash;

            string UID = "^JOY\\d{4}$";
            string phno = "^\\d{10}$";
            if (regestrationModel.UserId == "" || !Regex.IsMatch(regestrationModel.UserId, UID))
            {
                return BadRequest("User ID cannot be empty  and UserID must start with JOY followed by your 4 numbers");
            }
            var existingUser = _timesheet_Context.Register.FirstOrDefault(u => u.UserId == regestrationModel.UserId);
            if (existingUser != null)
            {
                return BadRequest("A user with the same UserId already exists");
            }
            if (!Regex.IsMatch(regestrationModel.PhoneNumber.ToString(), phno))
            {
                return BadRequest("Invalid phone number");
            }
            string Passwordpattern1 = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
            if (regestrationModel.Password == "" || !Regex.IsMatch(regestrationModel.Password, Passwordpattern1))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (regestrationModel.Password != regestrationModel.Confirmpassword)
            {
                return BadRequest("Confirm Password should match with Password");
            }
            return Ok(_timesheetInterface.Regester(regestrationModel));
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = _timesheet_Context.Register.FirstOrDefault(i => i.UserId == loginModel.UserId);
            if (user == null)
            {
                return BadRequest("UserId Not Exist..!!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            var password = _timesheet_Context.Register.FirstOrDefault(i => i.HashKeyPassword == passwordHash);
            if (password != null)
            {
                return BadRequest("Wrong Password");
            }
            return Ok(_timesheetInterface.Login(loginModel));
        }
        [HttpPut("Reset Password")]
        public IActionResult ResetPassword(LoginModel loginModel)
        {
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.UserId == loginModel.UserId);
            if (name == null)
            {
                return BadRequest("Username Not Existed..!!");
            }
            string Passwordpattern1 = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
            if (loginModel.Password == "" || !Regex.IsMatch(loginModel.Password, Passwordpattern1))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (loginModel.Password != loginModel.Confirmpassword)
            {
                return BadRequest("Confirm Password should match with Password");
            }
            return Ok(_timesheetInterface.ResetPassword(loginModel));
        }
    }
}
