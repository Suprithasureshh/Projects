using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Time_Log.Model
{
    public class TimeLog_Summary
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeLogSummary_Id { get; set; }

        [ForeignKey("Faculty_Id")]
        public int Faculty_Id { get; set; }

        [ForeignKey("Academic_Year_ID")]
        public int Academic_Year_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public int Year { get; set; }

        public double No_Of_days_Worked { get; set; }

        public double No_Of_Leave_Taken { get; set; }

        public double Total_Working_Hours { get; set; }
        public double No_Of_Days_Attended { get; set; }

        public double Total_ClassAttended_Hours { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Created_Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public Nullable<DateTime> Modified_Date { get; set; }
        public string? ImagePathUpload { get; set; }
        public string? ImagePathTimeLog { get; set; }
    }
}
