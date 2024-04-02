using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class HOD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HOD_Id { get; set; }

        [Required(ErrorMessage = "First name should not be empty")]
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

       
        [Required(ErrorMessage = "Official_Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string HOD_Email_Id { get; set; }

        [Required(ErrorMessage = "Contact_No is required")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Contact allows only digits.")]
        public string HOD_Contact_No { get; set; }

        public bool Is_Active { get; set; }
    }
}
