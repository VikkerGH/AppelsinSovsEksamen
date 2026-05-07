using Domain.Models;
using Domain.Persistence;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    // Specifikt repository for Game – bruges i stedet for den generiske InDatabasePersist<Game>,
    // fordi vi har brug for at hente Kategori med via Include (lazy loading er ikke slået til).
    public class GameRepository : IRepository<Game>
    {
        private readonly AppDbContext _db;

        public GameRepository(AppDbContext db)
        {
            _db = db;
        }

        // Include(g => g.Kategori) sikrer at Kategori-objektet ikke er null når vi læser spil
        public IEnumerable<Game> GetAll() =>
            _db.Games.Include(g => g.Kategori).AsNoTracking().ToList();

        public Game? GetById(Guid id) =>
            _db.Games.Include(g => g.Kategori).FirstOrDefault(g => g.Id == id);

        public void Add(Game entity)
        {
            _db.Games.Add(entity);
            _db.SaveChanges();
        }

        public void Update(Game entity)
        {
            _db.Games.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _db.Games.Find(id);
            if (entity is null) return;
            _db.Games.Remove(entity);
            _db.SaveChanges();
        }
    }
}
