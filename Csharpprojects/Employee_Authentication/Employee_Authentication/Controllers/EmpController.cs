using Employee_Authentication.DataContext;
using Employee_Authentication.Interface;
using Employee_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;

namespace Employee_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly EmployeeInterface _employeeInterface;
        private readonly Cont_Methods _cont_Methods;
        private readonly IConfiguration _configuration;

        public EmpController(Cont_Methods cont_Methods, EmployeeInterface employeeInterface, IConfiguration configuration)
        {
            _employeeInterface = employeeInterface;
            _cont_Methods = cont_Methods;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public IActionResult Register1([FromBody] Register register)
        {
            try
            {
                string pattern = "^[^0-9]*$";
                if (register.FirstName == "" || !Regex.IsMatch(register.FirstName, pattern))
                {
                    return BadRequest("User name cannot be empty  and  user name cannot contain number");
                }
                string Passwordpattern1 = "^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?])[A-Za-z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{8,}$";
                if (register.Password == "" || !Regex.IsMatch(register.Password, Passwordpattern1))
                {
                    return BadRequest("Password should contain first letter should capital letter and one special symbol");
                }
                if (register.Password != register.ConfirmPassword)
                {
                    return BadRequest("Conform Password should match with Password");
                }
                return Ok(_employeeInterface.Register1(register));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            try
            {
                var passwordValied = _cont_Methods.registers.FirstOrDefault(x => x.Password == login.Password);
                var Email = _cont_Methods.registers.FirstOrDefault(i => i.Email == login.Email);
                if (passwordValied != null && Email != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, login.Email)
                    };
                    var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        _configuration.GetSection("AppSettings:Token").Value!));
                    var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
                    var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(jwt);
                }
                return BadRequest("Invalied input..!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Add Role"), Authorize]
        public IActionResult Roles([FromBody] Role_Model role_Model)
        {
            try
            {
                var userId = _cont_Methods.registers.FirstOrDefault(i => i.EmpId == role_Model.EmpId);
                var rolename = _cont_Methods.role_Models.FirstOrDefault(i => i.RoleName == role_Model.RoleName);
                var roleId = _cont_Methods.role_Models.FirstOrDefault(i => i.RoleId == role_Model.RoleId);
                var roleName = _cont_Methods.role_Models.FirstOrDefault(i => i.RoleName == role_Model.RoleName);
                if (roleId != null || roleName != null || userId == null)
                {
                    return BadRequest("Role is already existed Or User id is wrong");
                }
                return _employeeInterface.Roles(role_Model);
            }
            catch (Exception)
            {
                throw new Exception("Unauthorized");
            }
        }
        [HttpPut("Update"), Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUser updateUser)
        {
            try
            {
                var Name = _cont_Methods.registers.FirstOrDefault(i => i.EmpId == updateUser.EmpId);
                if (Name == null)
                    return BadRequest("User Does not exists");
                return Ok(_employeeInterface.UpdateUser(updateUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete, Authorize]
        public IActionResult delete(int Id)
        {
            try
            {
                var d = _cont_Methods.registers.Where(i => i.Id == Id);
                if (d == null)
                    return BadRequest("record not available");
                return Ok(_employeeInterface.delete(Id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("multipleUpdate_true"), Authorize]
        public void UpdateMultipleData(long[] id)
        {
            try
            {
                _employeeInterface.UpdateMultipleData(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("multipleUpdate_false"), Authorize]
        public void UpdateMultipleData12(long[] id)
        {
            try
            {
                _employeeInterface.UpdateMultipleData12(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("pageNumber and pageSize and SearchKey and Emplyee"), Authorize]
        public IEnumerable<Register> GetEmplyee(int pageNumber, int pageSize, string? SearchKey, bool? active1)
        {
            try
            {
                return _employeeInterface.GetEmplyee(pageNumber, pageSize, SearchKey, active1);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("pageNumber and pageSize and SearchKey and role"), Authorize]
        public IEnumerable<Role_Model> GetRole(int pageNumber, int pageSize, string? SearchKey, bool? active1)
        {
            try
            {
                //var excelApp = new Excel.Application();
                //excelApp.Visible = false;
                //var workbook = excelApp.Workbooks.Add(Type.Missing);
                //var worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                //worksheet.Name = "My Worksheet";

                return _employeeInterface.GetRole(pageNumber, pageSize, SearchKey, active1);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpGet("Role and EmpId"), Authorize]
        public IEnumerable<RoleEmp> get()
        {
            return _cont_Methods.roleEmps.ToList();
        }
    }
}
