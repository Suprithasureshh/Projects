using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeInformation.Model
{
    public class EmployeeProperties
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
      
        public string? Gender { get; set; }
        public string? Designation { get; set; }
        public string? Experience { get; set; }
        public string? Languages { get; set; }
    }
}
