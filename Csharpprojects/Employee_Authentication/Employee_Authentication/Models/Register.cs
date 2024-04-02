using System.ComponentModel.DataAnnotations;

namespace Employee_Authentication.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Hashpassword { get; set; }
        public DateTime Register_Date { get; set; }= DateTime.Now;

        public bool IsActive { get; set; }
    }
}
