using System.ComponentModel.DataAnnotations;

namespace EmployeeRegisterDetails.Model
{
    public class EmployeeProperties
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name contains only alphabets.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; } = string.Empty;
        
        
        public string Image { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact allows only digits.")]
        public string Contact { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

     
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public bool isActive { get; set; }

    }
}
