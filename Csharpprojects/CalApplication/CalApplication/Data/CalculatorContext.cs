using CalApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace CalApplication.Data
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
        {
        }
        public DbSet<ArithmeticOperation> Calculator { get; set; }

       
    }
}
