using Microsoft.AspNetCore.Mvc;
using ToDo.Modal;

namespace ToDo.Interface
{
    public interface ITodo
    {
        ActionResult Post(TodoProperties list);
        IEnumerable<TodoProperties> Get();
        void Update(TodoProperties list);
        void Delete(int Id);
        IActionResult select(bool? Status);
    }
}
