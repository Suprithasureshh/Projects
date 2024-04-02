using Learnify.Model;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Interface
{
    public interface LearnifyInterface
    {
        //Dashboard
        IEnumerable<Dashboard> GetDashboard(string Email, int User_Id);
        //UserDetails
        void AddUserDetails(UserDetails adduserdetails);
        void EditUserDetails(UserDetails edituserdetails);
        IQueryable<UserDetails> GetAllUserDetails();
        void Delete(int Id);

        //LoginDetails
        IActionResult ResetPassword(string email, string? username, string otp, string newPassword);
    }
}

