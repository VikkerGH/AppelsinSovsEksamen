using Domain.Models;
using Domain.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _persist;
        private readonly PasswordHasher<User> _passwordHasher;// Vi bruger Microsoft.AspNetCore.Identity's PasswordHasher til at håndtere hashing og salting af passwords
        public UserService(IRepository<User> persist)
        {
            _persist = persist;
            _passwordHasher = new PasswordHasher<User>(); // Vi initialiserer PasswordHasher, som vi vil bruge til at hash'e passwords sikkert
        }
        public IEnumerable<User> GetAll() => _persist.GetAll();
        public User GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("User not found", nameof(id));
        public User Create(string name, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
            if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("Password required", nameof(plainPassword));
            // Opretter en ny bruger
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
            user.Password = _passwordHasher.HashPassword(user, plainPassword); // Hash'er og salter passwordet ved hjælp af PasswordHasher, som håndterer det sikkert
            _persist.Add(user);
            return user;
        }
        public void Edit(Guid id, string newName, string newPlainPassword)
        {
            var user = _persist.GetById(id)
                ?? throw new ArgumentException("User not found", nameof(id));
            if (!string.IsNullOrWhiteSpace(newName)) user.Name = newName.Trim();
            if (!string.IsNullOrWhiteSpace(newPlainPassword))
            {
                user.Password = _passwordHasher.HashPassword(user, newPlainPassword);
            }
            _persist.Update(user);
        }
        public void Delete(Guid id) => _persist.Delete(id);

    }
}
