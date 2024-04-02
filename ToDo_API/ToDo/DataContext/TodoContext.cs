using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDo.Modal;

namespace ToDo.DataContext
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoProperties> ToDoTable { get; set; }
    }
}
