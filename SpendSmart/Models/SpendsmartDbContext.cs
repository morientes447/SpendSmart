using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendsmartDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public SpendsmartDbContext(DbContextOptions<SpendsmartDbContext> options)
            : base(options)
        {
            
        }
    }
}
