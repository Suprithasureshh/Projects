using Time_Log.Model;
using static Time_Log.DomainModel.TimeLog_DomainModel;

namespace Time_Log.Interface
{
    public interface TimeLogInterface
    {
        //Branch
        void AddBranch(AddBranchModel model);
        void EditBranch(EditBranchModel editBranchModel);

        void EditBranchIsActive(IsActiveModel BranchIsActiveModel, bool Is_Active);
        IQueryable<Branch> GetByBranchId(int id);
        IEnumerable<GetAllBranchByFacultiesModel> GetAllBranchByFaculties();
        IEnumerable<BranchIsActiveModel> GetBranchIsActive(bool? isActive);
        IQueryable<Branch> GetAllBranch();

        //Subject

        void AddSubject(AddSubjectsModel addSubjectsModel);
        void EditSubject(EditSubjectsModel editSubjectsModel);
        void EditSubjectIsActive(IsActiveModel SubjectIsActiveModel, bool Is_Active);
        IQueryable<Subject> GetBySubjectId(int id);

       // IEnumerable<SubjectIsActiveModel> GetSubjectIsActive(bool? isActive);
         IEnumerable<GetAllSubjectsByFacultiesModel> GetAllSubjectsByFaculties();
        IQueryable<Subject> GetAllSubjects();

        //Designation
        void AddDesignation(AddDesignationModel postDesignationModel);
        void EditDesignation(EditDesignationModel editDesignationModel);
        void EditDesignationIsActive(IsActiveModel DesignationIsActiveModel, bool Is_Active);
        IQueryable<Designations> GetByDesignationId(int id);
        IEnumerable<DesignationIsActiveModel> GetDesignationIsActive(bool? isActive);
        IEnumerable<GetAllDesignationsByFacultiesModel> GetAllDesignationsByFaculties();
        IQueryable<Designations> GetAllDesignations();

        //FacultyType
        void AddFacultyType(AddFacultyTypeModel addFacultyTypeModel);
        void EditFacultyType(EditFacultyTypeModel editFacultyTypeModel);
        void EditFacultyTypeIsActive(IsActiveModel FacultyTypeIsActiveModel, bool Is_Active);
        IQueryable<FacultyType> GetByFacultyTypeId(int id);
        IEnumerable<FacultyTypeIsActiveModel> GetFacultyTypeIsActive(bool? isActive);
        IEnumerable<GetAllFacultyTypeByFacultiesModel> GetAllFacultyTypeByFaculties() ;
        IQueryable<FacultyType> GetAllFacultyTypes();

        //Role
        void AddRole(AddRoleModel addRoleModel);
        void EditRole(EditRoleModel editRoleModel);

        //Faculties
        void AddFaculties(AddFacultyModel addFacultyModel);
        void EditEmployee(EditFacultyModel editFacultyModel);
        void EditFacultyIsActive(IsActiveModel FacultyIsActiveModel, bool Is_Active);
        IQueryable<Faculties> GetByEmployeeId(int id);
        List<GetAllFacultyByDesIdFacTypeIdModel> GetAllFacultyByDesIdFacTypeId();
        IEnumerable<FacultyIsActiveModel> GetFacultyIsActive(bool? isActive);
        IQueryable<Faculties> GetAllEmployees();
    }
}
