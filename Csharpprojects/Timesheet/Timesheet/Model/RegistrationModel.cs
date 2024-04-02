using System.ComponentModel.DataAnnotations;

namespace Timesheet.Model
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int PhoneNumber { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Password { get; set; }

        public string HashKeyPassword { get; set; }


        public string Confirmpassword { get; set; }

    }
}
