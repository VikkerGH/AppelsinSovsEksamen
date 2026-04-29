using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class InMemoryPersist<T> : IRepository<T> where T : class
    {
        private readonly Dictionary<Guid, T> _storage = new();

        public void Add(T entity)
        {
            var idProp = typeof(T).GetProperty("Id");
            var id = (Guid)idProp!.GetValue(entity)!;
            _storage[id] = entity;
        }

        public IEnumerable<T> GetAll() => _storage.Values;

        public T? GetById(Guid id) =>
            _storage.TryGetValue(id, out var value) ? value : null;

        public void Update(T entity)
        {
            var id = (Guid)typeof(T).GetProperty("Id")!.GetValue(entity)!;
            _storage[id] = entity;
        }

        public void Delete(Guid id)
        {
            _storage.Remove(id);
        }
    }
}
