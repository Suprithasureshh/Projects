using E_Agriculture.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_Agriculture.DAL.Domain_Model.E_Agriculture_Domain_Model;

namespace E_Agriculture.BAL.Interface
{
    public interface ILogin
    {
        IActionResult Login(Login loginModel);
        Task GenerateOtp(GenerateOtpDM data);
        Task VerifyEmail(GenerateOtpDM data);
        Task VerifyOtp(VerifyOtpDM data);
        Task SetNewPassword(SetPasswordDM data);
        IActionResult GetUserProfile(string mail_id);
    }
}
