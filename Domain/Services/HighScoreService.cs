using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class HighScoreService
    {
        private readonly IRepository<HighScore> _persist;
        public HighScoreService (IRepository<HighScore> persist)
        {
            _persist = persist;
        }
        public IEnumerable<HighScore> GetAll() => _persist.GetAll();
        public HighScore GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("High score not found", nameof(id));
        public HighScore Create(string name, decimal price)
        {
            var game = new HighScore();
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
