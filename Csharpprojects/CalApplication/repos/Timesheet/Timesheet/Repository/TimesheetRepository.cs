using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheet.DataContext;
using Timesheet.Interface;
using Timesheet.Model;

namespace Timesheet.Repository
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
        public IActionResult Regester(RegistrationModel regestrationModel)
        {
            _timesheet_Context.Register.Add(regestrationModel);
            _timesheet_Context.SaveChanges();
            return Ok("Regestered Successfully");
        }

        public IActionResult Login(LoginModel loginModel)
        {

            List<Claim> claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, loginModel.UserId)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
           _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
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
            _timesheet_Context.Register.Update(name);
            _timesheet_Context.SaveChanges();
            return Ok("Password Reset Successfully");
        }
    }
}
