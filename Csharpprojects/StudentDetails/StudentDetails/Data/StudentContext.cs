using Microsoft.EntityFrameworkCore;
using StudentDetails.Model;

namespace StudentDetails.Data
{
    public class StudentContext: DbContext
    {
    public StudentContext(DbContextOptions options) :base(options)
        { 
        }
        public DbSet<Student> Students { get; set; }
    }
}
