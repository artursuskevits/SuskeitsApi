using Microsoft.EntityFrameworkCore;

namespace SuskeitsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<string> Toode { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}