using System.ComponentModel.DataAnnotations;

namespace Timesheet.Model
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Employee_Name { get; set; }
        [EmailAddress]
        public string Employee_Email { get; set; }

        public DateTime Joining_date { get; set; }

        public int Phone_Number { get; set; }
    }
}
