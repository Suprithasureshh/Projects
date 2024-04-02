using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agriculture.DAL.Domain_Model
{
    public class E_Agriculture_Domain_Model
    {
        public class GenerateOtpDM
        {
            public string? Email { get; set; }

        }

        public class VerifyOtpDM
        {
            public string? User_Name { get; set; }
            public string? OTP { get; set; }
        }



        public class SetPasswordDM
        {
            public string? User_Name { get; set; }
            public string? Otp { get; set; }
            public string? NewPassword { get; set; }
            public string? ConfirmPassword { get; set; }
        }
        public class BuyerDetails
        {
            public string BuyerName { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }
            public string RequiredCrops { get; set; }
            public int RequiredQuantity { get; set; }
            public string Location { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public string Date { get; set; }
          
        }
        }
}
