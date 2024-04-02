using Employee_Authentication.DataContext;
using Employee_Authentication.Interface;
using Employee_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_Authentication.Repository
{
    public class EmpRepository : ControllerBase, EmployeeInterface
    {
        private readonly Cont_Methods _cont_Methods;
        private readonly IConfiguration _configuration;

        public EmpRepository(Cont_Methods cont_Methods, IConfiguration configuration)
        {
            _cont_Methods = cont_Methods;
            _configuration = configuration;
        }
        public ActionResult<Register> Login(Login login)
        {
            try
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
            catch (Exception)
            {
                return BadRequest("Invalied input..!");
            }
        }

        public IActionResult Register1(Register register)
        {
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
                register.Hashpassword = passwordHash;
                _cont_Methods.registers.Add(register);
                _cont_Methods.SaveChanges();
                return Ok("Register Successfully..!");
            }
            catch (Exception)
            {
                return BadRequest("Register Failed..!");
            }
        }

        public IActionResult Roles(Role_Model role_Model)
        {
            try
            {
                var roleId = _cont_Methods.role_Models.FirstOrDefault(i => i.RoleId == role_Model.RoleId);
                if (roleId == null)
                {
                    _cont_Methods.role_Models.Add(role_Model);
                    _cont_Methods.SaveChanges();

                    RoleEmp roleEmp = new RoleEmp();
                    roleEmp.Role_id = role_Model.RoleId;
                    roleEmp.User_id = role_Model.EmpId;
                    _cont_Methods.roleEmps.Add(roleEmp);
                    _cont_Methods.SaveChanges();
                    return Ok("Data Added");
                }
                else
                {
                    _cont_Methods.role_Models.Update(role_Model);
                    _cont_Methods.SaveChanges();
                    return Ok("Data Updated");
                }
            }
            catch (Exception)
            {
                return BadRequest("Role is not added..!");
            }
        }
        public IActionResult UpdateUser(UpdateUser updateUser)
        {
            try
            {
                var Name = _cont_Methods.registers.FirstOrDefault(i => i.EmpId == updateUser.EmpId);
                if (Name == null)
                    return BadRequest("User Does not exists");
                _cont_Methods.registers.UpdateRange();
                _cont_Methods.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult delete(int id)
        {
            var d = _cont_Methods.registers.Where(i => i.Id == id);
            if (d == null)
            {
                return BadRequest("record not available");
            }
            var Item = _cont_Methods.registers.ToList();
            _cont_Methods.registers.RemoveRange();
            _cont_Methods.SaveChanges();
            return Ok("Deleted Successfully..!");
        }
        public void UpdateMultipleData(long[] id)
        {
            try
            {
                var newValues = new { Is_Active = true };
                var ToUpdate = _cont_Methods.registers.Where(p => id.Contains(p.Id));
                if (ToUpdate != null && ToUpdate.Any())
                {
                    foreach (var ToUpdate1 in ToUpdate)
                    {
                        ToUpdate1.IsActive = newValues.Is_Active;
                    }
                }
                _cont_Methods.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateMultipleData12(long[] id)
        {
            try
            {
                var newValues = new { Is_Active = false };
                var ToUpdate = _cont_Methods.registers.Where(p => id.Contains(p.Id));
                if (ToUpdate != null && ToUpdate.Any())
                {
                    foreach (var ToUpdate1 in ToUpdate)
                    {
                        ToUpdate1.IsActive = newValues.Is_Active;
                    }
                }
                _cont_Methods.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<Register> GetEmplyee(int pageNumber, int pageSize, string? SearchKey, bool? active1)
        {
            try
            {
                if (pageNumber == 0 || pageSize == 0 || SearchKey == null || active1 == null)
                {
                    return _cont_Methods.registers.ToList();
                }
                var products = _cont_Methods.registers;
                var itemsOnPage = products.Where(p => p.FirstName.Contains(SearchKey)).OrderBy(p => p.Id).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).Where(e => e.IsActive == active1).ToList();
                return itemsOnPage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<Role_Model> GetRole(int pageNumber, int pageSize, string? SearchKey, bool? active1)
        {
            try
            {
                if (pageNumber == 0 || pageSize == 0 || SearchKey == null || active1 == null)
                {
                    return _cont_Methods.role_Models.ToList();
                }
                var products = _cont_Methods.role_Models;
                var itemsOnPage = products.Where(p => p.RoleName.Contains(SearchKey)).OrderBy(p => p.Id).Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).Where(e => e.Activate == active1).ToList();
                return itemsOnPage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

