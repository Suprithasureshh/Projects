namespace Timesheet.Model
{
    public class UploadModel
    {
        public string Employee_Name;

        public int Id { get; set; }

        public string Day { get; set; }

        public string Status { get; set; }

        public int total_hours { get; set; }

        public DateTime Date { get; set; }

        public string month { get; set; }
    }
}
