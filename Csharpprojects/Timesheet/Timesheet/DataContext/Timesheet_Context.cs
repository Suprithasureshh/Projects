using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Timesheet.Model;

namespace Timesheet.DataContext
{
    public class Timesheet_Context : DbContext
    {
        public Timesheet_Context(DbContextOptions<Timesheet_Context> options) : base(options) { }
        public DbSet<RegistrationModel> Register { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<UploadModel> TS_table { get; set; }

        public DbSet<EmployeeModel> ETS_table { get; set; }
    }
}
