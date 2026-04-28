using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
