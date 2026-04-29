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

        // Test spil
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vi bruger faste Guids, så EF Core kan genkende dem ved hver opdatering
            modelBuilder.Entity<Game>().HasData(
                new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Appelsin Hop", Description = "Hop over forhindringerne og scor point!" },
                new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Hvor filan er appelsinen?", Description = "Find den skjulte appelsin så hurtigt som muligt. Ingen hjælp. Ingen hints. Den er der et sted" }
            );
        }
    }
}