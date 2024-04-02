using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class Semister
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Semister_Id { get; set; }

        [Required(ErrorMessage = "Branch Name should not be empty")]
        public string Semister_Name { get; set; }


    }
}
