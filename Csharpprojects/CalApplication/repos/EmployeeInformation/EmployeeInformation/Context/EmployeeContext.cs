using EmployeeInformation.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeInformation.Context
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmployeeProperties> Employee { get; set; }
    }
}
