using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agriculture.DAL.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "First name should not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "User Name should contains only alphabets.")]
        public string User_Name { get; set; }
        public string Address { get; set; }
       
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact_No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ContactNumber allows only digits.")]
        
        public string Contact_No { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Joining_Date { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Field should not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Field should contains only alphabets.")]
        public string UserAs { get; set; }

        public string Hashpassword { get; set; }

        public string? OTP { get; set; }

        
    }
}
