using Employee_Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Employee_Authentication.DataContext
{
    public class Cont_Methods : DbContext
    {
        public Cont_Methods(DbContextOptions<Cont_Methods> option) : base(option) { }

        public DbSet<Register> registers { get; set; }

        public DbSet<Role_Model> role_Models { get; set; }

        public DbSet<RoleEmp> roleEmps { get; set; }
    }
}
