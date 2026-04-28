using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}
        public DbSet<Game> Games => Set<Game>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
    }
}
