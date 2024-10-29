using Microsoft.EntityFrameworkCore;
using SuskeitsApi.Models;

namespace SuskeitsApi
{
    public class SuskeitsDbContext : DbContext
    {
        public SuskeitsDbContext(DbContextOptions<SuskeitsDbContext> options) : base(options) { }

        public DbSet<Toode> Tooted { get; set; }
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<KasutajaToode> KasutajaTooded { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the many-to-many relationship
            modelBuilder.Entity<KasutajaToode>()
                .HasKey(kt => new { kt.KasutajaId, kt.ToodeId });

            modelBuilder.Entity<KasutajaToode>()
                .HasOne(kt => kt.Kasutaja)
                .WithMany(k => k.KasutajaTooted)
                .HasForeignKey(kt => kt.KasutajaId);

            modelBuilder.Entity<KasutajaToode>()
                .HasOne(kt => kt.Toode)
                .WithMany(t => t.KasutajaTooted)
                .HasForeignKey(kt => kt.ToodeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
