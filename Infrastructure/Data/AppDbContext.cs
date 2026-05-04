using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<HighScore> HighScores => Set<HighScore>();
        public DbSet<Kategori> Kategorier => Set<Kategori>();

        // Seed data for spil
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasData(
                new { Id = Guid.Parse("a1b2c3d4-e5f6-4789-a012-3456789abcde"), Name = "Appelsin Hop", Description = "Hop over forhindringerne og scor point!" },
                new { Id = Guid.Parse("b2c3d4e5-f6a7-4890-b123-456789abcdef"), Name = "Hvor filan er appelsinen?", Description = "Find den skjulte appelsin så hurtigt som muligt. Ingen hjælp. Ingen hints. Den er der et sted" }
            );

            modelBuilder.Entity<User>().HasData(
    new
    {
        Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
        Name = "Test Admin",
        Email = "admin@test.dk",
        Password = "test123",
        CreatedAt = DateTime.UtcNow
    }
);
        }
    }
}