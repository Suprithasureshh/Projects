using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learnify.Model
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "User name should not be empty")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "User Name contains only alphabets.")]
        public string? UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Date_Of_Birth { get; set; }

        [Required(ErrorMessage = "Field Should not be empty")]
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zip_Code { get; set; }
        public string? Area_Code { get; set; }

        [Required(ErrorMessage = "Phone_No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "PhoneNumber allows only digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Confirm_Password { get; set; }
        public string? OTP { get; set; }
         public string? Hashpassword { get; set; }
        public string Status { get; set; }
    }
}
