using Learnify.Interface;
using Learnify.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using Learnify.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnifyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly LearnifyContext _login;
        private readonly LearnifyInterface _admin;

        public LearnifyController(IConfiguration configuration
            , LearnifyContext login, LearnifyInterface admin)
        {
            _configuration = configuration;
            _admin = admin;
            _login = login;
        }

        //Dashboard

        [HttpGet("GetDashboard")]

        public IEnumerable<Dashboard> GetDashboard(string Email, int User_Id)
        {
            return _admin.GetDashboard(Email, User_Id);
        }

        [HttpPost("AddUserDetails")]
        public void AddUserDetails(UserDetails model)
        {
            _admin.AddUserDetails(model);
        }

        [HttpPut("EditUserDetails")]
        public void EditUserDetails(UserDetails model)
        {
            _admin.EditUserDetails(model);
        }

        [HttpGet("GetAllUserDetails")]
        public IQueryable<UserDetails> GetAllUserDetails()
        {
            return _admin.GetAllUserDetails();
        }
        [HttpDelete("{DeleteUserDetailsById}")]
        public IActionResult Delete(int Id)
        {
            _admin.Delete(Id);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDetails loginModel)
        {
            var Email = await _login.userdetails.FirstOrDefaultAsync(i => i.Email == loginModel.Email);
            if (Email == null)
            {
                return NotFound("Email Not Found");
            }
            
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginModel.Password, Email.Hashpassword);
            if (!isPasswordCorrect)
            {
                return NotFound("Password Not Found");
            }
            List<Claim> claims = new List<Claim>
            {

                        new Claim(ClaimTypes.Email, loginModel.Email)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var y = _login.userdetails.FirstOrDefault(e => e.Email == loginModel.Email);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                User_Id = y.User_Id,
                
            });
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserDetails usermodel)
        {
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (usermodel.Email == "" || !Regex.IsMatch(usermodel.Email, email))
            {
                return BadRequest("Invalied Email");
            }
            var Email = await _login.userdetails.FirstOrDefaultAsync(i => i.Email == usermodel.Email);
            if (Email != null)
            {
                return BadRequest("User already exists");
            }
            var user = new UserDetails();
            user.UserName = usermodel.UserName;
            user.Date_Of_Birth = usermodel.Date_Of_Birth;
            user.Gender = usermodel.Gender;
            user.Address = usermodel.Address;
            user.City = usermodel.City;
            user.State = usermodel.State;
            user.Country = usermodel.Country;
            user.Zip_Code = usermodel.Zip_Code;
            user.Area_Code = usermodel.Area_Code;
            user.PhoneNumber = usermodel.PhoneNumber;
            user.Email = usermodel.Email;
            user.Password = usermodel.Password;
            user.Confirm_Password = usermodel.Confirm_Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usermodel.Password);
            user.Hashpassword = passwordHash;
            _login.userdetails.Add(user);
            _login.SaveChanges();
            return Ok("User Added Successfully..!");

        }

        //Generate OTP
        [HttpPost]
        [Route("GenerateOTP")]
        public IActionResult GeneratesOTP(string? email)
        {
            
            var emailCheck = _login.userdetails.FirstOrDefault(e => e.Email == email);
            // Generate a random 4-digit OTP
            Random random = new Random();
            string otp = (random.Next(1000, 9999)).ToString();
            if (email != "" )
            {
                if (emailCheck == null)
                {
                    return BadRequest("Entered Email is wrong");
                }
                
                if (emailCheck.Email != null)
                {
                    emailCheck.OTP = otp;
                    _login.SaveChanges();

                    // Send the OTP to the user via email 
                    var userName = emailCheck.UserName ;
                    string fromAddress = "learnifypoint@gmail.com";
                    string Password = "lhuytefzzaunguuj";
                    string toAddress = emailCheck.Email;
                    string emailHeader = "<html><body><h1>OTP to Reset Password</h1></body></html>";
                    string emailFooter = $"<html><head><title>Learnify</title></head><body><p>Hi {userName}, <br> This is the confidential email. Don't share your otp with anyone..!<br>  </p></body></html>";
                    string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your one time password(otp) is: <h3>{otp}</h3></p></body></html>";
                    string emailContent = emailHeader + emailBody + emailFooter;
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromAddress);
                    message.Subject = "Reset Password";
                    message.To.Add(new MailAddress(toAddress));
                    message.Body = emailContent;
                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromAddress, Password),
                        EnableSsl = true,
                    };

                    smtpClient.Send(message);
                    return Ok($"OTP generated and sent to MailID: '{emailCheck.Email}'");
                }
            }
            return Ok("Entered Email is wrong");
        }
        
        //verify Passwrd
        [HttpPost]
        [Route("VerifyOTP")]
        public IActionResult VerifyOTP(string? email, string? username, string otp, string newPassword)
        {
            var result = _admin.ResetPassword(email, username, otp, newPassword);
            if (result is OkObjectResult)
            {
                return Ok("Password reset successful");
            }
            else
            {
                return result;
            }
        }

    }
}
