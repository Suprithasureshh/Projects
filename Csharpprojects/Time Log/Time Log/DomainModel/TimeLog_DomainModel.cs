using System.ComponentModel.DataAnnotations;

namespace Time_Log.DomainModel
{
    public class TimeLog_DomainModel
    {
        //BranchModel
        public class AddBranchModel
        {
            public string Branch_Name { get; set; }
        }
        public class EditBranchModel
        {
            public int Branch_Id { get; set; }
            public string Branch_Name { get; set; }
        }
        public class GetAllBranchByFacultiesModel
        {
            public int Branch_Id { get; set; }
            public string Branch_Name { get; set; }
            public int No_Of_Faculties { get; set; }
        }

        //SubjectModel
        public class AddSubjectsModel
        {
            public string Subject_Name { get; set; }
            public string Subject_Code { get; set; }
            public int Branch_Id { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Subject_Start_Date { get; set; }

        }

        public class EditSubjectsModel
        {
            public int Subject_Id { get; set; }
            public string Subject_Name { get; set; }
            public string? Notes { get; set; }
            public string? VideoLink { get; set; }
            public int Semister_Id { get; set; }
            public int Branch_Id { get; set; }


            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Start_Date { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime End_Date { get; set; }
        }

        public class GetAllSubjectsByFacultiesModel
        {
            public int Subject_Id { get; set; }
            public string Subject_Name { get; set; }
            public string Subject_Code { get; set; }
            public string? Notes { get; set; }
            public string? VideoLink { get; set; }
            public int No_Of_Faculties { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Start_Date { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime End_Date { get; set; }
        }

        //DesignationModel
        public class AddDesignationModel
        {
            public string Designation { get; set; }
        }
        public class EditDesignationModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
        }
        public class GetAllDesignationsByFacultiesModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
            public int No_of_Faculties { get; set; }
        }


       
        //FacultyType
        public class AddFacultyTypeModel
        {
            public string Faculty_Type { get; set; }
        }
        public class EditFacultyTypeModel
        {
            public int FacultyType_Id { get; set; }
            public string Faculty_Type { get; set; }
        }
        public class GetAllFacultyTypeByFacultiesModel
        {
            public int FacultyType_Id { get; set; }
            public string Faculty_Type { get; set; }
            public int No_of_Faculties { get; set; }
        }

        //RoleModels

        public class AddRoleModel
        {
            public string Role { get; set; }
        }
        public class EditRoleModel
        {
            public int Role_Id { get; set; }
            public string Role { get; set; }
        }

        //FacultyModel

        public class AddFacultyModel
        {
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Faculty_code { get; set; }
            public string HOD { get; set; }
            public int FacultyType_Id { get; set; }

            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Official_Email { get; set; }
            public string? Alternate_Email { get; set; }

            public int Designation_Id { get; set; }
            public string Contact_No { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Joining_Date { get; set; }

        }

        public class EditFacultyModel
        {
            public int Faculty_Id { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Faculty_code { get; set; }
            public string HOD { get; set; }
            public int FacultyType_Id { get; set; }
            public string Designation { get; set; }
            public string Faculty_Type { get; set; }

            [Required(ErrorMessage = "Official_Email field is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Official_Email { get; set; }
            public string? Alternate_Email { get; set; }
            public string Contact_No { get; set; }

      
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime End_Date { get; set; }
        }

        public class GetAllFacultyByDesIdFacTypeIdModel
        {
            public int Faculty_Id { get; set; }
            public string First_Name { get; set; }
            public string last_Name { get; set; }
            public string Full_Name { get; set; }
            public string Faculty_code { get; set; }
            public string HOD { get; set; }
            public int FacultyType_Id { get; set; }
            public string Faculty_Type { get; set; }
            public string Official_Email { get; set; }
            public string Alternate_Email { get; set; }
            public int Role_Id { get; set; }
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
            public string Contact_No { get; set; }
            public DateTime Joining_Date { get; set; }
            public DateTime End_Date { get; set; }
        }

        //HOD Model

        public class AddHODContactModel
        {
            public string HOD_Email_Id { get; set; }
        }

        public class EditHODContactModel
        {
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public int HOD_Id { get; set; }
            public string HOD_Email_Id { get; set; }
            public string HOD_Contact_No { get; set; }
        }
        public class HODcontactByFacultyModel
        {
            public int HOD_Id { get; set; }
            public string Name { get; set; }
            public string Designation { get; set; }
            public string Joining_Date { get; set; }
            public string Email { get; set; }
            public string Mobile_No { get; set; }
        }

        //EditIsActive
        public class IsActiveModel
        {
            public int[] Id { get; set; }
        }
        //GetIsActive

        public class BranchIsActiveModel
        {
            public int Branch_Id { get; set; }
            public string Branch_Name { get; set; }
            public bool Is_Active { get; set; }
            public int No_Of_Faculties { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Create_Date { get; set; }


        }
        public class DesignationIsActiveModel
        {
            public int Designation_Id { get; set; }
            public string Designation { get; set; }
            public bool Is_Active { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]

            public int No_Of_Faculties { get; set; }

        }
        public class SubjectIsActiveModel
        {
            public int Subject_Id { get; set; }
            public string Subject_Name { get; set; }
            public string Subject_Code { get; set; }
            public int Branch_Id { get; set; }
            public int No_Of_Faculties { get; set; }
            public bool Is_Active { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Subject_Start_Date { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
            public DateTime Subject_End_Date { get; set; }




        }
        public class FacultyTypeIsActiveModel
        {
            public int FacultyType_Id { get; set; }
            public string Faculty_Type { get; set; }
            public int No_Of_Faculties { get; set; }
            public bool Is_Active { get; set; }

        }
        public class FacultyIsActiveModel
        {
            public int Faculty_Id { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Full_Name { get; set; }
            public string Faculty_code { get; set; }
            public string HOD { get; set; }
            public string Faculty_Type { get; set; }
            public string Official_Email { get; set; }
            public int Role_Id { get; set; }
            public string Designation { get; set; }
            public string Contact_No { get; set; }
            public DateTime Joining_Date { get; set; }
            public DateTime End_Date { get; set; }
            public bool Is_Active { get; set; }
        }

        public class HODInfoIsActiveModel
        {
            public int HOD_Id { get; set; }
            public string HOD { get; set; }
            public string HOD_Email_Id { get; set; }
            public string HOD_Contact_No { get; set; }
            public bool Is_Active { get; set; }
        }

    }
}
