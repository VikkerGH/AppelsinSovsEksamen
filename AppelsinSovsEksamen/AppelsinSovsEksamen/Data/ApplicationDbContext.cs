using Microsoft.EntityFrameworkCore;

namespace AppelsinSovsEksamen.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
