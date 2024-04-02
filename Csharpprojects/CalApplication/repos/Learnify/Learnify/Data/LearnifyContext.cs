using Learnify.Model;
using Microsoft.EntityFrameworkCore;

namespace Learnify.Data
{
    public class LearnifyContext : DbContext
    {
        public LearnifyContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserDetails> userdetails { get; set; }
    }
}
