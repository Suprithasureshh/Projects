using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace E_Agriculture.DAL.Model
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Answer_Id { get; set; }
        public string AnswerFor { get; set; }
        
       
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Answer_Date { get; set; }

        [ForeignKey("User_Id")]
        public int User_Id { get; set; }

        [ForeignKey("Question_Id")]
        public int Question_Id { get; set; }
    }
}
