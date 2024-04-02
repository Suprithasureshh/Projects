using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDetails.Data;
using StudentDetails.Model;

namespace StudentDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void create(Student std)
        {
            _context.Students.Add(std);
            _context.SaveChanges();
        }

        [HttpPut("{Id}")]
        public void update(Student std)
        {
            _context.Students.Update(std);
            _context.SaveChanges();
        }
        [HttpDelete("{Id}")]

        public void delete(Student std)
        {
            _context.Students.Remove(std);
            _context.SaveChanges();
        }
        [HttpGet]
        public IEnumerable<Student> studentsget()
        {
            return _context.Students;
        }
        
        
    }
}
