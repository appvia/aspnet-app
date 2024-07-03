using Microsoft.EntityFrameworkCore;

namespace WebApi.MainDbContext
{
    public class MainDemoDbContext : DbContext
    {
        public MainDemoDbContext(DbContextOptions<MainDemoDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
