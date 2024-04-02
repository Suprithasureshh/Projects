using System.ComponentModel.DataAnnotations;

namespace Employee_Authentication.Models
{
    public class Login
    {
        
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
