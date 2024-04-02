using E_Agriculture.DAL.Model;
using Microsoft.EntityFrameworkCore;


namespace E_Agriculture.DAL.Data_Context
{
    public class E_Agriculture_Context : DbContext
    {
        public E_Agriculture_Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> userdetails { get; set; }
        public DbSet<Answer> answer { get; set; }
        public DbSet<AvailabilityCrops> availabilityCrops { get; set; }
        public DbSet<GovernmentPrograms> governmentPrograms { get; set; }
        public DbSet<MarketDetails> marketDetails { get; set; }
        public DbSet<RequiredCrops> requiredCrops { get; set; }
        public DbSet<Queries> queries { get; set; }
    
    }
}
