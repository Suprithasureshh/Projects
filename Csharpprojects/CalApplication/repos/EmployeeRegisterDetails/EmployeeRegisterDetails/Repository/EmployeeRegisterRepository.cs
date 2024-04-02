using EmployeeRegisterDetails.DataContext;
using EmployeeRegisterDetails.Interface;
using EmployeeRegisterDetails.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EmployeeRegisterDetails.Repository
{
    public class EmployeeRegisterRepository : ControllerBase, IEmployeeRegister
    {
        private readonly EmployeeRegisterContext _context;
        public EmployeeRegisterRepository(EmployeeRegisterContext context)
        {
            _context = context;
        }
        public ActionResult Post(EmployeeProperties emp)
        {

            if (_context.EmployeeRegister.Any(b => b.Email == emp.Email))
            {
                return BadRequest($"Email is already exists ");
            }
            else if(_context.EmployeeRegister.Any(b => b.Contact == emp.Contact))
            {
                return BadRequest($"Contact is already exists ");
            }
            else
            {
                _context.EmployeeRegister.Add(emp);
                _context.SaveChanges();
                return Ok(_context.EmployeeRegister);
            }

        }
        public IEnumerable<EmployeeProperties> Get()
        {
            return _context.EmployeeRegister;
        }
        public void UpdateEmployee(EmployeeProperties emp)
        {
            var obj = _context.EmployeeRegister.Find(emp.Id);
            if (obj != null)
            {
                obj.Name = emp.Name;
                obj.Email = emp.Email;
                obj.Image = emp.Image;
                obj.Contact = emp.Contact;
                obj.Address = emp.Address;
                obj.RegisterDate = emp.RegisterDate;
                obj.isActive = emp.isActive;
               // _context.EmployeeRegister.Update(obj);
                _context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            var emp = _context.EmployeeRegister.Find(Id);

            _context.EmployeeRegister.Remove(emp);
            _context.SaveChanges();

        }
        public void ActivateEmployee(int[] id)
        {
            var records = _context.EmployeeRegister.Where(a => id.Contains(a.Id) && !a.isActive);
            foreach (var record in records)
            {
                record.isActive = true;
            }
            _context.SaveChanges();
        }

        public void DeactivateEmployee(int[] id)
        {
            var records = _context.EmployeeRegister.Where(a => id.Contains(a.Id) && a.isActive);
            foreach (var record in records)
            {
                record.isActive = false;
            }
            _context.SaveChanges();
        }
        public IActionResult select(bool? isActive)
        {
            IQueryable<EmployeeProperties> emp = _context.EmployeeRegister;

            if (isActive != null )
            {
                var activeEmp = isActive == true ? emp.Where(b => (bool)b.isActive) : emp.Where(b => (bool)!b.isActive);
                return Ok(activeEmp);
            }
            
            else
            {
                return Ok(_context.EmployeeRegister.ToList());
            }

        }
    }
}
