using Microsoft.EntityFrameworkCore;
using ProductApplication.Model;
using System.Collections.Generic;

namespace ProductApplication.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
            {
            }
            public DbSet<ProductProperties> ProductTable { get; set; }
        }
    
}
