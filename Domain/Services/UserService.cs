using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _persist;
        public UserService(IRepository<User> persist)
        {
            _persist = persist;
        }
        public IEnumerable<User> GetAll() => _persist.GetAll();
        public User GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("User not found", nameof(id));
        public User Create(string name, decimal price)
        {
            var user = new User();
            _persist.Add(user);
            return user;
        }
        public void Edit(Guid id, string newName, decimal newPrice)
        {
            var user = _persist.GetById(id)
                ?? throw new ArgumentException("User not found", nameof(id));
            _persist.Update(user);
        }
        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }

    }
}
