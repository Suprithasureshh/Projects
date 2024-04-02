using EmployeeRegisterDetails.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeRegisterDetails.DataContext
{
    public class EmployeeRegisterContext:DbContext
    {
        public EmployeeRegisterContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmployeeProperties> EmployeeRegister { get; set; }
    }
}