using EbillApplication.Interface;
using EbillApplication.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EbillApplication.Data
{
    public class EbillContext : DbContext
    {
        public EbillContext(DbContextOptions<EbillContext> options) : base(options)
        {
        }
        public DbSet<EbillProperties> EbillTable { get; set; }
    }
}
