using System.ComponentModel.DataAnnotations;

namespace ToDo.Modal
{
    public class TodoProperties
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.Date)]
        public DateTime Due_Date { get; set; }
        public bool Status { get; set; }
    }
}
