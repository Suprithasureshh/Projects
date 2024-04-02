using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class TimeLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeLog_Id { get; set; }

        [ForeignKey("Faculty_Id")]
        public int? Faculty_Id { get; set; }

        public int? Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public string Day { get; set; }

        public bool Leave { get; set; }

        [ForeignKey("Subject_Id")]
        public int? Subject_Id { get; set; } = null;

        [ForeignKey("TimeLogSummary_Id")]
        public int TimeLogSummary_Id { get; set; }

        public int Duration_in_Hours { get; set; }
    }
}
