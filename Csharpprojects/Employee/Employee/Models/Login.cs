using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Login
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
