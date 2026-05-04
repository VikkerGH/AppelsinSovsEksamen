using Domain.Persistence;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class InDatabasePersist<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _set;
        public InDatabasePersist(AppDbContext db)
        {
            _db = db;
            _set = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _set.Add(entity);
            _db.SaveChanges();
        }

        public IEnumerable<T> GetAll() => _set.AsNoTracking().ToList();

        public T? GetById(Guid id) => _set.Find(id);

        public void Update(T entity)
        {
            _set.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var entity = _set.Find(id);
            if (entity is null) return;
            _set.Remove(entity);
            _db.SaveChanges();

            // Detach entiteten for at undgå tracking conflicts
            _db.Entry(entity).State = EntityState.Detached;
        }
    }
}
