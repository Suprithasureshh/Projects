using E_Agriculture.BAL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static E_Agriculture.DAL.Domain_Model.E_Agriculture_Domain_Model;
using E_Agriculture.DAL.Model;
using E_Agriculture.DAL.Data_Context;

namespace E_Agriculture.BAL.Implementation
{
    public class LoginRepo:ControllerBase, ILogin
    {
        private readonly E_Agriculture_Context _context;
        private readonly IConfiguration _configuration;
        Random random = new Random();
        public LoginRepo(E_Agriculture_Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Login(Login loginModel)
        {
            var user = _context.userdetails.FirstOrDefault(i => i.Email == loginModel.Email);

            if (user == null)
            {
                throw new EmailException();
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Hashpassword);

            if (!isPasswordCorrect)
            {
                throw new Password();
            }

            // Check if the user role (UserAs) matches the expected role
            if (user.UserAs != loginModel.UserAs)
            {
                // Handle the case where the user role does not match
                throw new UserAsException();
            }

            // If the user has the correct email, password, and user role, proceed to generate the JWT token
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, loginModel.Email),
        new Claim("Email", loginModel.Email),
        new Claim("User_Id", user.User_Id.ToString()),
        new Claim("User_Name", user.User_Name),
        new Claim("Address", user.Address),
        new Claim("UserAs", user.UserAs),
    };

            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }

        public async Task GenerateOtp(GenerateOtpDM data)
        {
            var emailCheck = await _context.userdetails.FirstOrDefaultAsync(x => x.Email.Equals(data.Email));



            if (emailCheck == null)
            {
                throw new EmailException();
            }
            else
            {
                string otp = (random.Next(1000, 9999)).ToString();
                emailCheck.OTP = otp;
                await _context.SaveChangesAsync();



                // Send the OTP to the user via email
                string fromAddress = "learnifypoint@gmail.com";
                string Password = "lhuytefzzaunguuj";
                string toAddress = emailCheck.Email;
                string emailHeader = "<html><body><h1>OTP for setting new password</h1></body></html>";
                string emailFooter = $"<html><head><title>LearnifyPoint</title></head><body><p>Hi {emailCheck.User_Name}, <br> This is the confidential email. Don't share your OTP with anyone..!<br> </p></body></html>";
                string emailBody = $"<html><head><title>Don't reply to this Mail</title></head><body><p>Your one-time password (OTP) is: <h3>{emailCheck.OTP}</h3></p></body></html>";
                string emailContent = emailHeader + emailBody + emailFooter;



                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(fromAddress);
                    message.Subject = "Forgot Password";
                    message.To.Add(new MailAddress(toAddress));
                    message.Body = emailContent;
                    message.IsBodyHtml = true;



                    using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(fromAddress, Password);
                        smtpClient.EnableSsl = true;
                        await smtpClient.SendMailAsync(message);
                    }
                }
                // To remove the OTP after 60 seconds
                await Task.Delay(TimeSpan.FromSeconds(60));
                emailCheck.OTP = "";
                await _context.SaveChangesAsync();
            }
        }

        public async Task VerifyEmail(GenerateOtpDM data)
        {

            var emailCheck = await _context.userdetails.FirstOrDefaultAsync(x => x.Email.Equals(data.Email));

            if (emailCheck == null)
            {
                throw new EmailException();
            }
        }
        public async Task VerifyOtp(VerifyOtpDM data)
        {
            var emailCheck = await _context.userdetails.FirstOrDefaultAsync(x => x.Email.Equals(data.User_Name));
            if (emailCheck.OTP == data.OTP)
            {
                emailCheck.OTP = "";
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOtp();
            }
        }



        public async Task SetNewPassword(SetPasswordDM data)
        {
            var emailCheck = await _context.userdetails.FirstOrDefaultAsync(x => x.Email.Equals(data.User_Name) || x.User_Name.Equals(data.User_Name));

            if (emailCheck == null)
            {
                throw new EmailException();
            }
            
            emailCheck.Password = data.NewPassword;
            
            var hash = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
            emailCheck.Hashpassword = hash;
            await _context.SaveChangesAsync();

        }
        public IActionResult GetUserProfile(string mail_id)
        {
            var query = from ud in this._context.userdetails
                        where ud.Email == mail_id
                        select new
                        {
                            ud.User_Id,
                            ud.User_Name,
                            ud.Email,
                            ud.Joining_Date,
                            ud.Contact_No,
                            ud.Address,
                            ud.UserAs
                        };

            var userProfile = query.FirstOrDefault();

            if (userProfile == null)
            {
                throw new DataNotFound();
            }

            return Ok(userProfile);
        }
    }
}
