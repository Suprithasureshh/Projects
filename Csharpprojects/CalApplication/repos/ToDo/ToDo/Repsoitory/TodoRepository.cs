using Microsoft.AspNetCore.Mvc;
using ToDo.DataContext;
using ToDo.Interface;
using ToDo.Modal;

namespace ToDo.Repsoitory
{
    public class TodoRepository:ControllerBase, ITodo
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public ActionResult Post(TodoProperties list)
        {
            _context.ToDoTable.Add(list);
            _context.SaveChanges();
            return Ok(_context.ToDoTable);
            
        }
        public IEnumerable<TodoProperties> Get()
        {
            return _context.ToDoTable;
        }
        public void Update(TodoProperties list)
        {
            var obj = _context.ToDoTable.Find(list.Id);
            if (obj != null)
            {
                obj.Title = list.Title;
                obj.Description = list.Description;
                obj.Due_Date = list.Due_Date;
                obj.Status = list.Status;
                _context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            var list = _context.ToDoTable.Find(Id);
            _context.ToDoTable.Remove(list);
            _context.SaveChanges();

        }
        public IActionResult select(bool? Status)
        {
            IQueryable<TodoProperties> emp = _context.ToDoTable;

            if (Status != null)
            {
                var activeEmp = Status == true ? emp.Where(b => (bool)b.Status) : emp.Where(b => (bool)!b.Status);
                return Ok(activeEmp);
            }

            else
            {
                return Ok(_context.ToDoTable.ToList());
            }

        }
    }
}
