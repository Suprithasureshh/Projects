using Learnify.Data;
using Learnify.Interface;
using Learnify.Model;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using Learnify.Exceptions;

namespace Learnify.Repository
{
    public class LearnifyRepo :ControllerBase, LearnifyInterface
    {
        private readonly LearnifyContext _context;
        public LearnifyRepo(LearnifyContext context)
        {
            _context = context;
        }
        public IEnumerable<Dashboard> GetDashboard(string Email, int User_Id)
        {
            var data = new Dashboard();
            var item1 = (from ud in _context.userdetails
                         where (ud.Email == Email && ud.User_Id == User_Id)
                         group ud by ud.Status into g
                         select new Dashboard
                         {
                             x = g.Key,
                             y = g.Count()
                         });
            return item1;
        }
        public void AddUserDetails(UserDetails adduserdetails)
        {
            var EmailContactCheck = _context.userdetails.FirstOrDefault(e => e.Email == adduserdetails.Email || e.PhoneNumber == adduserdetails.PhoneNumber);

            if (EmailContactCheck == null)
            {
                if (adduserdetails.Password != adduserdetails.Confirm_Password)
                {
                    throw new ConfirmpasswordException();
                }
                var user =new UserDetails();
                user.UserName = adduserdetails.UserName;
                user.Date_Of_Birth = adduserdetails.Date_Of_Birth;
                user.Gender = adduserdetails.Gender;
                user.Address = adduserdetails.Address;
                user.City = adduserdetails.City;
                user.State = adduserdetails.State;
                user.Country = adduserdetails.Country;
                user.Zip_Code = adduserdetails.Zip_Code;
                user.Area_Code = adduserdetails.Area_Code;
                user.PhoneNumber = adduserdetails.PhoneNumber;
                user.Email = adduserdetails.Email;
                user.Password = adduserdetails.Password;
                user.Confirm_Password = adduserdetails.Confirm_Password;
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Hashpassword = passwordHash;
                user.OTP = "";

                _context.userdetails.Add(adduserdetails);
                _context.SaveChanges();

                var fullname = user.UserName;
                string fromAddress = "learnifypoint@gmail.com";
                string Password = "lhuytefzzaunguuj";
                string toAddress = adduserdetails.Email;
                string emailHeader = "<html><body><h1>Congratulations</h1></body></html>";
                string emailFooter = $"<html><head><title>Learnify</title></head><body><p>Hi {fullname}, <br> This is the confidential email. Don't share your password with anyone..!<br> Click here to change your credentials  <a href=\"http://localhost:3000/\"> : http://localhost:3000/ </a> </p></body></html>";
                string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your password is: {user.Password}</p></body></html>";
                string emailContent = emailHeader + emailBody + emailFooter;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromAddress);
                message.Subject = "Welcome To Learnify Point";
                message.To.Add(new MailAddress(toAddress));
                message.Body = emailContent;
                message.IsBodyHtml = true;

                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, Password),
                    EnableSsl = true,
                };

              smtpClient.Send(message);
            }
            else
            {
                throw new EmailPhonenoException();
            }

        }
        public IQueryable<UserDetails> GetAllUserDetails()
        {
            return _context.userdetails.AsQueryable();
        }

        public void EditUserDetails(UserDetails edituserdetails)
        {
            var user = _context.userdetails.FirstOrDefault(e => e.User_Id == edituserdetails.User_Id);
            if (user != null)
            {
                user.UserName = edituserdetails.UserName;
                user.Date_Of_Birth = edituserdetails.Date_Of_Birth;
                user.Gender = edituserdetails.Gender;
                user.Address = edituserdetails.Address;
                user.City = edituserdetails.City;
                user.State = edituserdetails.State;
                user.Country = edituserdetails.Country;
                user.Zip_Code = edituserdetails.Zip_Code;
                user.Area_Code = edituserdetails.Area_Code;
                user.PhoneNumber = edituserdetails.PhoneNumber;
                user.Email = edituserdetails.Email;
               
               _context.SaveChanges();
            }
        }


        public void Delete(int Id)
        {
            var list = _context.userdetails.Find(Id);
            _context.userdetails.Remove(list);
            _context.SaveChanges();

        }

        //LoginDetails

        public IActionResult ResetPassword(string email, string? username, string otp, string newPassword)
        {
            var data = _context.userdetails.FirstOrDefault(i => i.OTP == otp && i.Email == email || i.UserName == username);
            if (data == null)
            {
                return BadRequest("Invalid OTP");
            }

            string Passwordpattern = "^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?])[A-Za-z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{8,}$";
            if (!Regex.IsMatch(newPassword, Passwordpattern))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }

            data.Password = newPassword;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            data.Hashpassword = passwordHash;
            data.OTP = ""; // clear the OTP
            _context.SaveChanges();
            return Ok("Password is reset");
        }
    }
}
