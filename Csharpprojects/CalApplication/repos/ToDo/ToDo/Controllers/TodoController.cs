using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ToDo.Interface;
using ToDo.Modal;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodo _repository;
        public TodoController(ITodo repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Post(TodoProperties list)
        {
            return _repository.Post(list);
        }
        [HttpGet]
        public IEnumerable<TodoProperties> Gets()
        {
            return _repository.Get();
        }
        [HttpPut("Update")]

        public IActionResult Update(TodoProperties list)
        {
            _repository.Update(list);
            return NoContent();
        }
        
        [HttpGet("Pending and Complete")]
        public IActionResult select(bool? Status)
        {
            return _repository.select(Status);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _repository.Delete(Id);
            return Ok();
        }

    }
}
