using E_Agriculture.BAL.Implementation;
using E_Agriculture.BAL.Interface;
using E_Agriculture.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using static E_Agriculture.DAL.Domain_Model.E_Agriculture_Domain_Model;

namespace E_Agriculture.Controllers
{
    [Route("api/[controller" +
        "]")]
    [ApiController]
    [CustomExceptionFilter]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _admin;

        public LoginController(ILogin admin)
        {

            _admin = admin;

        }
        [HttpPost("Login")]

        public IActionResult LoginUser(Login loginModel)
        {
            return _admin.Login(loginModel);
        }

        //VerifyEmail

        [HttpPost("VerifyEmail")]

        public async Task VerifyEmail(GenerateOtpDM data)
        {
            await _admin.VerifyEmail(data);

        }

        //Generate OTP
        [HttpPost("GenerateOTP")]

        public async Task GenerateOtp(GenerateOtpDM data)
        {
            await _admin.GenerateOtp(data);

        }



        //verify Passwrd
        [HttpPost("VerifyOTP")]
        public async Task VerifyOtp(VerifyOtpDM data)
        {
            await _admin.VerifyOtp(data);

        }

        //set newpassword

        [HttpPost("SetNewPassword")]
        public async Task SetNewPassword(SetPasswordDM data)
        {
            await _admin.SetNewPassword(data);

        }

        //getting userprofile

        [HttpGet("UserProfile")]
        public IActionResult GetUserProfile(string mail_id)
        {
            return _admin.GetUserProfile(mail_id);
        }
    }
}
