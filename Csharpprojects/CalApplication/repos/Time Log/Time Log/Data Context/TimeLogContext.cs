using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Time_Log.Model;

namespace Time_Log.Data_Context
{
    public class TimeLogContext : DbContext
    {
        public TimeLogContext(DbContextOptions<TimeLogContext> options) : base(options) { }

        public DbSet<Branch> branch { get; set; }
        public DbSet<Designations> designations { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Faculties> faculties { get; set; }
        public DbSet<FacultyType> facultytypes { get; set; }
        public DbSet<Roles> roles { get; set; }
    }
}
