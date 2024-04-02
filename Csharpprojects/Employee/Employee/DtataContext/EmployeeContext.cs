using Employee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;

namespace Employee.DtataContext
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        { }
        public DbSet<EmployeeRegister> EmployeeDetails { get; set; }

        public DbSet<Roles> RolesDetails { get; set; }

        public DbSet<Role_Emp> RolesEmployee { get; set; }



    }



}
