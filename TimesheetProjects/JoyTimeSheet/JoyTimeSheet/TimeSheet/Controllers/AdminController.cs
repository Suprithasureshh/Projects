using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TimeSheet.Implementation.DomainModel.AdminDomainModel;
using TimeSheet.Model;
using TimeSheet.Implementation.Interface;
using TimeSheet.ExceptionFilter;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin; 
        }

        //Clients

        [HttpPost("AddClient")]
        public void AddClient(AddClientModel model)
        {
            _admin.AddClient(model);
        }

        [HttpPut("EditClient")]
        public void EditClient(EditClientModel editClientModel)
        {
            _admin.EditClient(editClientModel);
        }

        [HttpGet("GetByClientId")]
        public IQueryable<Client> GetByClientId(int id)
        {
            return _admin.GetByClientId(id);
        }

        [HttpGet("GetAllClients")]
        public IQueryable<Client> GetAllClients()
        {
            return _admin.GetAllClients();
        }

        //Designation

        [HttpPost("AddDesignation")]
        public void AddDesignation(PostDesignationModel postDesignationModel)
        {
            _admin.AddDesignation(postDesignationModel);
        }

        [HttpPut("EditDesignation")]
        public void EditDesignation(EditDesignationModel editDesignationModel)
        {
            _admin.EditDesignation(editDesignationModel);
        }

        [HttpGet("GetByDesignationId")]
        public IQueryable<Designations> GetByDesignationId(int id)
        {
            return _admin.GetByDesignationId(id);
        }

        [HttpGet("GetAllDesignationsByEmployee")]
        public IEnumerable<GetAllDesignationsByEmployeeModel> GetAllDesignationsByEmployee()
        {
            return _admin.GetAllDesignationsByEmployee();
        }

        [HttpGet("GetAllDesignations")]
        public IQueryable<Designations> GetAllDesignations()
        {
            return _admin.GetAllDesignations();
        }

        //EmployeeType

        [HttpPost("AddEmployeeType")]
        public void AddEmployeeType(PostEmployeeTypeModel postEmployeeTypeModel)
        {
            _admin.AddEmployeeType(postEmployeeTypeModel);
        }

        [HttpPut("EditEmployeetype")]
        public void EditEmployeetype(EditEmployeeTypeModel editEmployeeTypeModel)
        {
            _admin.EditEmployeetype(editEmployeeTypeModel);
        }

        [HttpGet("GetByEmployeeTypeId")]
        public IQueryable<EmployeeType> GetByEmployeeTypeId(int id)
        {
            return _admin.GetByEmployeeTypeId(id);
        }

        [HttpGet("GetAllEmployeeTypesByEmployee")]
        public IEnumerable<GetAllEmployeeTypeByEmployeeModel> GetAllEmployeeTypesByEmployee()
        {
            return _admin.GetAllEmployeeTypesByEmployee();
        }
        
        [HttpGet("GetAllEmplyoeeTypes")]
        public IQueryable<EmployeeType> GetAllEmplyoeeTypes()
        {
            return _admin.GetAllEmplyoeeTypes();
        }
    }
}
