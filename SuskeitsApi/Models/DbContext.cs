using Microsoft.EntityFrameworkCore;

namespace SuskeitsApi.Models
{
    public class SuskeitsDbContext : DbContext
    {
        public SuskeitsDbContext(DbContextOptions<SuskeitsDbContext> options) : base(options) { }

        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Toode> Tooted { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Toode>()
                .HasOne(t => t.Kasutaja)
                .WithMany(k => k.Tooted)
                .HasForeignKey(t => t.KasutajaId);
        }
    }
}
