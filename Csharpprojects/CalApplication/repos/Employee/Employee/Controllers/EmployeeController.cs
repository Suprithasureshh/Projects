using CsvHelper;
using Employee.DtataContext;
using Employee.Interface;
using Employee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Formats.Asn1;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Employee.Repository.EmployeeRepository;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _repository;
        private readonly EmployeeContext _context;
        private readonly IConfiguration _configuration;

      
        public EmployeeController(IConfiguration configuration, IEmployee employee, EmployeeContext context)
        {
            _configuration = configuration;
            _context = context;
            _repository = employee;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Registers(EmployeeRegister request)
        {
            return await _repository.Register(request);
        }

        [HttpPost("Login")]

        public async Task<ActionResult<string>> Logins(Login request)
        {
            return await _repository.Login(request);
        }

        [HttpGet]

        public IQueryable<EmployeeRegister> Selects(string? search, bool isActive, int page = 1, int pagesize = 10)
        {
            return _repository.Selects(search, isActive, page, pagesize);
        }
        [ServiceFilter(typeof(LoggingFilter))]
        [HttpPost("Roles")]

        public IActionResult Roles(Roles role)
        {
            if (_context.RolesDetails.Any(b => b.Name == role.Name))
            {
                throw new UserException2();
            }
            else
            {
                _repository.Roles(role);
                return Ok();
            }
        }
       
        [HttpPut("UpdateEmployee")]

        public void UpdateEmployee(int id,EmployeeRegister user)
        {
            if(id!=user.Id)
            {
                throw new UserException1();
            }
            else
            {
                _repository.UpdateEmployee(user);
            }
            
        }

        [HttpPut("ActivateUser")]

        public void updateAct(int[] Id)
        {
            _repository.ActiveEmployee(Id);
        }

        [HttpPut("InactivateUser")]

        public void updateInact(int[] Id)
        {
            _repository.DeactiveEmployee(Id);
        }

        [HttpPut("ActivateRole")]

        public void updateActive(int[] Id)
        {
            _repository.ActivateRole(Id);
        }

        [HttpPut("InactiveRole")]

        public void updateInactive(int[] Id)
        {
            _repository.DeactivateRole(Id);
        }

        [HttpGet("EmployeeBasedOnRole")]
       
        public IQueryable<EmployeeRegister> UsersBasedOnRoles(int? Roleid, string? search, bool isActive, int page = 1, int pagesize = 10)
        {
            return _repository.EmployeeBasedOnRole(Roleid, search, isActive, page, pagesize);
        }

       
        [HttpGet("GetAll")]

        public IQueryable<EmployeeRegister> SelectAll()
        {
             return _repository.GetAll();
        }

        [HttpGet("export")]

        public IActionResult ExportEmployees()
        {
            var employees = _repository.GetAll();

            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(employees);

                var content = writer.ToString();
                var bytes = Encoding.UTF8.GetBytes(content);

                var result = new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "EmployeeDetails.csv"
                };

                return result;
            }
        }



    }
}

