using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Time_Log.Interface;
using Time_Log.Model;
using static Time_Log.DomainModel.TimeLog_DomainModel;

namespace Time_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {

        private readonly TimeLogInterface _admin;
        public TimeLogController(TimeLogInterface admin)
        {
            _admin = admin;
        }

        ////Dashboard

        //[HttpGet("GetDashboard")]

        //public IEnumerable<GetDashboardModel> GetDashboard(int year, int Month_Id)
        //{
        //    return _admin.GetDashboard(year, Month_Id);
        //}

        //Branch

        [HttpPost("AddBranch")]
        public void AddBranch(AddBranchModel model)
        {
            _admin.AddBranch(model);
        }

        [HttpPut("EditBranch")]
        public void EditClient(EditBranchModel editBranchModel)
        {
            _admin.EditBranch(editBranchModel);
        }

        [HttpPut("EditBranchIsActive")]
        public void EditBranchIsActive(IsActiveModel BranchIsActiveModel, bool Is_Active)
        {
            _admin.EditBranchIsActive(BranchIsActiveModel, Is_Active);
        }

        [HttpGet("GetByBranchId")]
        public IQueryable<Branch> GetByBranchId(int id)
        {
            return _admin.GetByBranchId(id);
        }

        [HttpGet("GetClientIsActive")]
        public IEnumerable<BranchIsActiveModel> GetBranchIsActive(bool? isActive)
        {
            return _admin.GetBranchIsActive(isActive);
        }


        [HttpGet("GetAllBranchByFaculties")]
        public IEnumerable<GetAllBranchByFacultiesModel> GetAllBranchByFaculties()
        {
            return _admin.GetAllBranchByFaculties();
        }

        [HttpGet("GetAllBranch")]
        public IQueryable<Branch> GetAllBranch()
        {
            return _admin.GetAllBranch();
        }


        //Subjects

        [HttpPost("AddSubject")]
        public void AddSubject(AddSubjectsModel addSubjectsModel)
        {
            _admin.AddSubject(addSubjectsModel);
        }

        [HttpPut("EditSubject")]
        public void EditSubject(EditSubjectsModel editSubjectsModel)
        {
            _admin.EditSubject(editSubjectsModel);
        }

        [HttpPut("EditSubjectIsActive")]
        public void EditSubjectIsActive(IsActiveModel SubjectIsActiveModel, bool Is_Active)
        {
            _admin.EditSubjectIsActive(SubjectIsActiveModel, Is_Active);
        }

        [HttpGet("GetBySubjectId")]
        public IQueryable<Subject> GetBySubjecttId(int id)
        {
            return _admin.GetBySubjectId(id);
        }

        //[HttpGet("GetSubjectIsActive")]
        //public IEnumerable<SubjectIsActiveModel> GetSubjectIsActive(bool? isActive)
        //{
        //    return _admin.GetSubjectIsActive(isActive);
        //}

        [HttpGet("GetAllSubjectsByEmployee")]
        public IEnumerable<GetAllSubjectsByFacultiesModel> GetAllSubjectsByEmployee()
        {
            return _admin.GetAllSubjectsByFaculties();
        }

        [HttpGet("GetAllSubjects")]
        public IQueryable<Subject> GetAllSubjects()
        {
            return _admin.GetAllSubjects();
        }


        //Designation
        [HttpPost("AddDesignation")]
        public void AddDesignation(AddDesignationModel addDesignationModel)
        {
            _admin.AddDesignation(addDesignationModel);
        }

        [HttpPut("EditDesignation")]
        public void EditDesignation(EditDesignationModel editDesignationModel)
        {
            _admin.EditDesignation(editDesignationModel);
        }

        [HttpPut("EditDesignationIsActive")]
        public void EditDesignationIsActive(IsActiveModel DesignationIsActiveModel, bool Is_Active)
        {
            _admin.EditDesignationIsActive(DesignationIsActiveModel, Is_Active);
        }

        [HttpGet("GetByDesignationId")]
        public IQueryable<Designations> GetByDesignationId(int id)
        {
            return _admin.GetByDesignationId(id);
        }

        [HttpGet("GetDesignationIsActive")]
        public IEnumerable<DesignationIsActiveModel> GetDesignationIsActive(bool? isActive)
        {
            return _admin.GetDesignationIsActive(isActive);
        }

        [HttpGet("GetAllDesignationsByFaculties")]
        public IEnumerable<GetAllDesignationsByFacultiesModel> GetAllDesignationsByFaculties()
        {
            return _admin.GetAllDesignationsByFaculties();
        }

        [HttpGet("GetAllDesignations")]
        public IQueryable<Designations> GetAllDesignations()
        {
            return _admin.GetAllDesignations();
        }

        //FacultyType

        [HttpPost("AddFacultyType")]
        public void AddFacultyType(AddFacultyTypeModel addFacultyTypeModel)
        {
            _admin.AddFacultyType(addFacultyTypeModel);
        }

        [HttpPut("EditFacultyType")]
        public void EditFacultyType(EditFacultyTypeModel editFacultyTypeModel)
        {
            _admin.EditFacultyType(editFacultyTypeModel);
        }

        [HttpPut("EditFacultyTypeIsActive")]
        public void EditFacultyTypeIsActive(IsActiveModel FacultyTypeIsActiveModel, bool Is_Active)
        {
            _admin.EditFacultyTypeIsActive(FacultyTypeIsActiveModel, Is_Active);
        }

        [HttpGet("GetByFacultyTypeId")]
        public IQueryable<FacultyType> GetByFacultyTypeId(int id)
        {
            return _admin.GetByFacultyTypeId(id);
        }

        [HttpGet("GetFacultyTypeIsActive")]
        public IEnumerable<FacultyTypeIsActiveModel> GetFacultyTypeIsActive(bool? isActive)
        {
            return _admin.GetFacultyTypeIsActive(isActive);
        }

        [HttpGet("GetAllFacultyTypesByEmployee")]
        public IEnumerable<GetAllFacultyTypeByFacultiesModel> GetAllFacultyTypeByFaculties()
        {
            return _admin.GetAllFacultyTypeByFaculties();
        }

        [HttpGet("GetAllFacultyTypes")]
        public IQueryable<FacultyType> GetAllFacultyTypes()
        {
            return _admin.GetAllFacultyTypes();
        }
    }
}
