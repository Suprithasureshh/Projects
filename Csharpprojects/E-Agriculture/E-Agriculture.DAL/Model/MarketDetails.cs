using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agriculture.DAL.Model
{
    public class MarketDetails
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MCrop_Id { get; set; }

        [Required(ErrorMessage = "Crop name should not be empty")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Crop Name should contains only alphabets.")]
        public string MCrop_Name { get; set; }
        public int MQuantity { get; set; }
        public int MPrice { get; set; }
        public string MLocation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime MAdd_Date { get; set; }
    }
}
