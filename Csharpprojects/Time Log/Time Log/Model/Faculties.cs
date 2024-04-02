using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class Faculties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Faculty_Id { get; set; }

        [Required(ErrorMessage = "First name should not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name should contains only alphabets.")]
        public string First_Name { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name should contains only alphabets.")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Faculty Code is required")]
        public string Faculty_code { get; set; }

        public string HOD { get; set; }

        [ForeignKey("FacultyType_Id")]
        public int FacultyType_Id { get; set; }

        [Required(ErrorMessage = "Official_Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Official_Email { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Alternate_Email { get; set; }

        [ForeignKey("Branch_Id")]
        public int Branch_Id { get; set; }

        [ForeignKey("Subject_Id")]
        public int Subject_Id { get; set; }

        [ForeignKey("Semister_Id")]
        public int Semister_Id { get; set; }

        [ForeignKey("Designation_Id")]
        public int Designation_Id { get; set; }

        [ForeignKey("Role_Id")]
        public int Role_Id { get; set; }

        [Required(ErrorMessage = "Contact_No is required")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Contact allows only digits.")]
        public string Contact_No { get; set; }

        public bool Is_Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Joining_Date { get; set; }

        public string Password { get; set; }

        public string Hashpassword { get; set; }

        public string Otp { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Create_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? End_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? Modified_Date { get; set; }
    }
}
