using EbillApplication.Interface;
using EbillApplication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbillApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbillController : ControllerBase
    {
        private readonly IEbill _repository;
        public EbillController(IEbill repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<string> BillGeneration(EbillProperties bill)
        {   
            //if (_repository.GetId(bill.CustomerId) != null)
            //{
            //    return BadRequest("Bill with this ID is already exists");
            //}

            return _repository.BillCalculate(bill);
        }

      
        [HttpGet("isActive")]
        public IEnumerable<EbillProperties> Get1(bool? isActive)
        {
           
            return _repository.Get1(isActive);   
        }
        [HttpDelete]
        public IActionResult delete(int CustomerId)
        {
            _repository.delete(CustomerId);
            return NoContent();
        }
        [HttpPut]
        public IActionResult update( [FromBody] EbillProperties Bill)
        {
            _repository.update(Bill);
            return NoContent();
        }
        [HttpPut("Active")]
        public IActionResult UpdateActive([FromBody] int[] ids)
        {
            _repository.UpdateActive(ids);

            return NoContent();
        }

        [HttpPut("NotActive")]
        public IActionResult UpdateNotActive([FromBody] int[] ids)
        {
            _repository.UpdateNotActive(ids);

            return NoContent();
        }
    }
}
