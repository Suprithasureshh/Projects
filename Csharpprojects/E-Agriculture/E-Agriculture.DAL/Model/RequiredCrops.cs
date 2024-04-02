using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agriculture.DAL.Model
{
    public class RequiredCrops
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RCrop_Id { get; set; }

        [Required(ErrorMessage = "Crop name should not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Crop Name should contains only alphabets.")]
        public string RCrop_Name { get; set; }
        public int RQuantity { get; set; }
        
        public string RLocation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime RAdd_Date { get; set; }

        [ForeignKey("User_Id")]
        public int User_Id { get; set; }
    }
}
