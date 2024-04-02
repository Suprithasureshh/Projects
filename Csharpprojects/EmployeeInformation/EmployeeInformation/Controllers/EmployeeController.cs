using EmployeeInformation.Interface;
using EmployeeInformation.Model;
using EmployeeInformation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _repository;
        public EmployeeController(IEmployee repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public ActionResult post(EmployeeProperties emp)
        {
            return _repository.post(emp);
        }
        [HttpGet]
        public IEnumerable<EmployeeProperties> Gets()
        {
            return _repository.Get();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _repository.Delete(Id);
            return Ok();
        }
    }
}
