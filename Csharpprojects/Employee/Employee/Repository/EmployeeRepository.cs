using Employee.DtataContext;
using Employee.Interface;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Employee.Repository
{
    public class EmployeeRepository : ControllerBase, IEmployee
    {

        public static EmployeeUser employee = new EmployeeUser();
        public static Login login = new Login();

        private readonly IConfiguration _configuration;
        private readonly EmployeeContext _context;

        public EmployeeRepository(IConfiguration configuration, EmployeeContext context)
        {
            _configuration = configuration;
            _context = context;

        }
        public async Task<ActionResult<string>> Register(EmployeeRegister request)
        {
            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            employee.Email = request.Email;
            employee.PasswordHash = PasswordHash;
            employee.PasswordSalt = PasswordSalt;
            login.Email = request.Email;
            login.Password = request.Password;


            var EmailCheck = _context.EmployeeDetails.FirstOrDefault(i => i.Email == request.Email);
            if (EmailCheck == null)
            {
                _context.EmployeeDetails.Add(request);
                _context.SaveChanges();

                Role_Emp roleemp = new Role_Emp();
                roleemp.RoleId = request.RoleId;
                roleemp.UserId = request.Id;
                _context.RolesEmployee.Add(roleemp);
                _context.SaveChanges();
                return "Data Added Succesfully";

            }
            else
            {
                return "Email already exists";

            }

        }
        public async Task<ActionResult<string>> Login(Login request)
        {
            if (login.Email != request.Email)
            {  

                return "Email not found";
            }
            if (!VerifyPasswordHash(request.Password, employee.PasswordHash, employee.PasswordSalt))
            {
                return "Your Password is incorrect!";
            }

            string token = CreateToken(employee);
            return token;

        }

        public IQueryable<EmployeeRegister> Selects(string? search, bool? isActive, int page = 1, int pagesize = 10)
        {
            var emp = _context.EmployeeDetails.AsQueryable();
            switch ((search, page, pagesize, isActive))
            {
                case (not null, 0, 0, null):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    return emp;

                case (not null, not 0, not 0, null):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    var pagedEmployee = emp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmployee;

                case (not null, 0, 0, not null):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    var activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    return activeEmp;

                case (not null, not 0, not 0, not null):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    pagedEmployee = activeEmp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmployee;

                case (null, not 0, not 0, null):
                    pagedEmployee = emp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmployee;

                case (null, 0, 0, not null):
                    activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    return activeEmp;
                default:
                    return _context.EmployeeDetails;
            }
        }

        public void Roles(Roles role)
        {
            _context.RolesDetails.Add(role);
            _context.SaveChanges();

            
        }

        public void UpdateEmployee(EmployeeRegister employee)
        {
            EmployeeRegister obj = new EmployeeRegister();
            obj.Id = employee.Id;
            obj.Name = employee.Name;
            obj.Address = employee.Address;
            obj.Password = employee.Password;

            _context.EmployeeDetails.Update(obj);
            _context.SaveChanges();
        }

        public void ActiveEmployee(int[] id)
        {
            var records = _context.EmployeeDetails.Where(a => id.Contains(a.Id) && !a.isActive);
            foreach (var record in records)
            {
                record.isActive = true;
            }
            _context.SaveChanges();
        }

        public void DeactiveEmployee(int[] id)
        {
            var records = _context.EmployeeDetails.Where(a => id.Contains(a.Id) && a.isActive);
            foreach (var record in records)
            {
                record.isActive = false;
            }
            _context.SaveChanges();
        }

        public void ActivateRole(int[] id)
        {
            var records = _context.RolesDetails.Where(a => id.Contains(a.Id) && !a.isActive);
            foreach (var record in records)
            {
                record.isActive = true;
            }
            _context.SaveChanges();
        }

        public void DeactivateRole(int[] id)
        {
            var records = _context.RolesDetails.Where(a => id.Contains(a.Id) && a.isActive);
            foreach (var record in records)
            {
                record.isActive = false;
            }
            _context.SaveChanges();
        }

        public IQueryable<EmployeeRegister> EmployeeBasedOnRole(int? Roleid, string? search, bool? isActive, int page = 1, int pagesize = 10)
        {
            var emp = _context.EmployeeDetails.AsQueryable();
            switch ((search, page, pagesize, isActive, Roleid))
            {
                case (not null, 0, 0, null, 0):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    return emp;

                case (not null, not 0, not 0, null, 0):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    var pagedEmp = emp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmp;

                case (not null, 0, 0, not null, 0):
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    var activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    return activeEmp;

                case (not null, not 0, not 0, not null, not 0):
                    emp = _context.EmployeeDetails.Where(p => p.RoleId.Equals(search));
                    emp = _context.EmployeeDetails.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                    activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    pagedEmp = activeEmp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmp;

                case (null, not 0, not 0, null, 0):
                    pagedEmp = emp.Skip((page - 1) * pagesize).Take(pagesize);
                    return pagedEmp;

                case (null, 0, 0, not null, 0):
                    activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                    return activeEmp;

                case (null, 0, 0, null, not 0):
                    emp = _context.EmployeeDetails.Where(p => p.RoleId.Equals(search));
                    return emp;

                default:
                    return _context.EmployeeDetails;
            }

        }

        public IQueryable<EmployeeRegister> GetAll()
        {
            return _context.EmployeeDetails;
        }

        private string CreateToken(EmployeeUser emp)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, emp.Email),
                new Claim(ClaimTypes.Role,"Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(employee.PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return ComputedHash.SequenceEqual(PasswordHash);
            }
        }
        public class UserException1 : Exception
        {
           public UserException1() { }
           public UserException1(string message) : base(message) { }
        }
        public class UserException2 : Exception
        {
            public UserException2() { }
            public UserException2(string message) : base(message) { }
        }
        public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                
                if (context.Exception is UserException1)
                {
                    context.Result = new BadRequestObjectResult("There is No data with Id");
                }
                else if (context.Exception is UserException2)
                {
                    context.Result = new BadRequestObjectResult("This Name is already Exists in the table");
                }
                else if (context.Exception is ArgumentNullException)
                {
                    context.Result = new BadRequestObjectResult("Argument Null Exception exception occurred.");
                }
                context.ExceptionHandled = true;
            }
        }

       
        



    }
}