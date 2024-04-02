using EmployeeInformation.Context;
using EmployeeInformation.Interface;
using EmployeeInformation.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace EmployeeInformation.Repository
{
    public class EmployeeRepository: ControllerBase, IEmployee
    {

        // private static List<EmployeeProperties> _context = new List<EmployeeProperties> { };
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        public ActionResult post(EmployeeProperties emp)
        {
            Regex regexE = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (_context.Employee.Any(b => b.Email == emp.Email))
            {
                return BadRequest($"Email is already exists ");
            }
            else if (emp.Email == "")
            {
                return BadRequest("Email cannot be null");
            }

            else if (!regexE.IsMatch(emp.Email))
            {
                return BadRequest("Email should be in formate");
            }
            else
            {
                _context.Employee.Add(emp);
                _context.SaveChanges();
                return Ok(_context.Employee);
            }

        }
        public IEnumerable<EmployeeProperties> Get()
        {
            return _context.Employee;
        }
        public void Delete(int Id)
        {
            var emp = _context.Employee.Find(Id);
           
                _context.Employee.Remove(emp);
                _context.SaveChanges();
            
        }
    }
}
