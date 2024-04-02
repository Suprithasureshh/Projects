using System.ComponentModel.DataAnnotations;

namespace TimesheetPoject.Model
{
    public class RegistrationModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="User ID cannot be empty")]
        [RegularExpression("^JOY\\d{4}$",ErrorMessage = " UserID must start with JOY followed by your 4 numbers")]

        public string UserId { get; set; }
        [Required(ErrorMessage = "User Name cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "User name must contain only alphabets")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email format")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }
        [RegularExpression("^\\(?\\d{3}\\)?[-.\\s]?\\d{3}[-.\\s]?\\d{4}$",
            ErrorMessage ="Invalid Phone number")]

        public int PhoneNumber { get; set; }
        public DateTime DateOfJoin { get; set; }
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
          ErrorMessage = "Invalid password pattern (Eg, Abcd!@#$%^&*1234)")]
        public string Password { get; set; }

        public string HashKeyPassword { get; set; }

        
        public string Confirmpassword { get; set; }



    }
}