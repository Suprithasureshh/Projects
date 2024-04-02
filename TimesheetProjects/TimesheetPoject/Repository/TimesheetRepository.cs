using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Repository
{
    public class TimesheetRepository : ControllerBase, ITimesheetInterface
    {
        private readonly Timesheet_Context _timesheet_Context;
        private readonly IConfiguration _configuration;

        public TimesheetRepository(Timesheet_Context timesheet_Context, IConfiguration configuration)
        {
            _timesheet_Context = timesheet_Context;
            _configuration = configuration;
        }
        public IActionResult Register(RegistrationModel regestrationModel)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(regestrationModel.Password);
            regestrationModel.HashKeyPassword = passwordHash;

            var existingUser = _timesheet_Context.Register.FirstOrDefault(u => u.Email == regestrationModel.Email);
            if (existingUser != null)
            {
                return BadRequest("Employee with the same Email already exists");
            }
            if (regestrationModel.Password != regestrationModel.Confirmpassword)
            {
                return BadRequest("Confirm Password should match with Password");
            }
            _timesheet_Context.Register.Add(regestrationModel);
            _timesheet_Context.SaveChanges();
            return Ok("Regestered Successfully");
        }

        public IActionResult Login(LoginModel loginModel)
        {
            var user = _timesheet_Context.Register.FirstOrDefault(i => i.UserId == loginModel.UserId);
            if (user == null)
            {
                return BadRequest("User does not Exist..!!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            var password = _timesheet_Context.Register.FirstOrDefault(i => i.HashKeyPassword == passwordHash);
            if (password != null)
            {
                return BadRequest("Wrong Password");
            } RegistrationModel users= new RegistrationModel();
            List<Claim> claims = new List<Claim>
            {                
                    new Claim(ClaimTypes.Name, loginModel.UserId, loginModel.Password, users.Email)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
           _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(2), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwt);
        }
        public IActionResult ResetPassword(LoginModel loginModel)
        {
            var name = _timesheet_Context.Register.FirstOrDefault(i => i.UserId == loginModel.UserId);
            name.UserId = loginModel.UserId;
            name.Password = loginModel.Password;          
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password);
            name.HashKeyPassword = passwordHash;       
            if (name == null)
            {
                return BadRequest("Username Not Exist..!!");
            }
            if (loginModel.Password != loginModel.Confirmpassword)
            {
                return BadRequest("Passwords Dont Match!");
            }
            _timesheet_Context.Register.Update(name);
            _timesheet_Context.SaveChanges();
            return Ok("Password Reset Successfully");
        }
    }
}