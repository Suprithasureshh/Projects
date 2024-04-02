using Microsoft.EntityFrameworkCore;
using TimesheetPoject.Model;

namespace TimesheetPoject.Context_Timesheet
{
    public class Timesheet_Context : DbContext
    {
        public Timesheet_Context(DbContextOptions<Timesheet_Context> options) : base(options) { }
        public DbSet<RegistrationModel> Register { get; set; }       
        public DbSet<UploadModel> TS_table { get; set; }
        public DbSet<EmployeeModel> ETS_table { get; set; }
    }
}