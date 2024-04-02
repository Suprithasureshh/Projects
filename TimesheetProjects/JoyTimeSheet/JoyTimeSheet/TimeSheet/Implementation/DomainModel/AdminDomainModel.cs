using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Implementation.DomainModel
{
    public class AdminDomainModel
    {
        //ClientModel
        public class AddClientModel
        {
            public string Client_Name { get; set; }
        }
        public class EditClientModel
        {
            public int Client_Id { get; set; }
            public string Client_Name { get; set; }
        }

        //DesignationModel

        public class PostDesignationModel
        {   
            public string Designation { get; set; }
        }
        public class EditDesignationModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
        }
        public class GetAllDesignationsByEmployeeModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
            public int No_of_Employees { get; set; }
        }

        //EmployeeType
        public class PostEmployeeTypeModel
        {
            public string Employee_Type { get; set; }          
        }
        public class EditEmployeeTypeModel
        {
            public int Employee_Type_Id { get; set; }
            public string Employee_Type { get; set; }
        }
        public class GetAllEmployeeTypeByEmployeeModel
        {
            public int Employee_Type_Id { get; set; }
            public string Employee_Type { get; set; }
            public int No_of_Employees { get; set; }
        }

        //employee

        public class AddEmployeeModel
        {
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Employee_code { get; set; }
            public string Reporting_Manager1 { get; set; }
            public string Reportinng_Manager2 { get; set; }
            public int Employee_Type_Id { get; set; }
            public string Official_Email { get; set; }
            public string Alternate_Email { get; set; }
            public string Password { get; set; }
            public int Designation_Id { get; set; }
            public string Contact_No { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Joining_Date { get; set; }
            public DateTime? End_Date { get; set;}
        }

        public class EditEmployeeModel
        {
            public int Employee_Id { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Employee_code { get; set; }
            public string Reporting_Manager1 { get; set; }
            public string Reportinng_Manager2 { get; set; }
            public int Employee_Type_Id { get; set; }
            public string Official_Email { get; set; }
            public string Alternate_Email { get; set; }
            public int Designation_Id { get; set; }
            public string Contact_No { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Joining_Date { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime End_Date { get; set; }
        }

        public class EmployeeModel
        {
            public int Employee_Id { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_code { get; set; }
            public string Reporting_Manager1 { get; set; }
            public string Reportinng_Manager2 { get; set; }
            public int Employee_Type_Id { get; set; }
            public string Email { get; set; }
            public string Alternate_Email { get; set; }
            public int Designation_Id { get; set; }
            public string Contact_No { get; set; }
            public DateTime Joining_Date { get; set; }
            public DateTime End_Date { get; set; }

        }

       

        public class GetAllEmployeeModel
        {
            public int Employee_Id { get; set; }
            public string First_Name { get; set; }
            public string last_Name { get; set; }
            public string Employee_code { get; set; }
            public string Reporting_Manager1 { get; set; }
            public string Reportinng_Manager2 { get; set; }
            public int Employee_Type_Id { get; set; }
            public string Employee_Type_Name { get; set; }
            public string Email { get; set; }
            public string Alternate_Email { get; set; }
            public int Role_Id { get; set; }
            public int Designation_Id { get; set; }
            public string Designation_Name { get; set; }
            public string Contact_No { get; set; }
            public DateTime Joining_Date { get; set; }
            public DateTime End_Date { get; set; }
        }

















    }
}
