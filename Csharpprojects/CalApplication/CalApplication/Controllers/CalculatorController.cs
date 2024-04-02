using CalApplication.Interface;
using CalApplication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _repository;

        public CalculatorController(ICalculator repository)
        {
            _repository = repository;
        }

        [HttpPost]

        public string add(ArithmeticOperation cal)
        {
            return _repository.PerformOperation(cal);
        }



        [HttpGet]
        public IEnumerable<ArithmeticOperation> Getall()
        {
            return _repository.Get();
        }
        [HttpDelete]
        public void Delete()
        {
             _repository.delete();
        }
    }
}
