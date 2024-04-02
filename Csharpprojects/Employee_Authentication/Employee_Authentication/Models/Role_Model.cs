using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Authentication.Models
{
    public class Role_Model
    {
        public int Id { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("EmpId")]
        public string EmpId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Activate { get; set; }
    }
}
