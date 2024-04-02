using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class ScoreTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Score_Id { get; set; }

        [ForeignKey("Student_Id")]
        public int Student_Id { get; set; }

        [ForeignKey("Semister_Id")]
        public int Semister_Id { get; set; }
        
        public string? Total_Marks { get; set; }
        public string? Marks_Obtained { get; set; }

    }
}
