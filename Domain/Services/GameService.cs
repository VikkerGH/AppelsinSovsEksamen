using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class GameService
    {
        private readonly IRepository<Game> _persist;  
        public GameService(IRepository<Game> persist)
        {
            _persist = persist;
        }
        public IEnumerable<Game> GetAll() => _persist.GetAll();
        public Game GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("Game not found", nameof(id));
        public Game Create(string name, decimal price)
        {
            var game = new Game();
            _persist.Add(game);
            return game;
        }
        public void Edit(Guid id, string newName, decimal newPrice)
        {
            var game = _persist.GetById(id)
                ?? throw new ArgumentException("Game not found", nameof(id));
            _persist.Update(game);
        }
        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }
    }
}

