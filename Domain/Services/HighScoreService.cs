using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddHighScore(HighScore newScore)
        {
            // Hent eksisterende highscores for dette spil
            var existing = _persist.GetAll()
                .Where(hs => hs.GameId == newScore.GameId)
                .OrderByDescending(hs => hs.Score)
                .ToList();

            // Hvis der er mindre end 5, tilføj altid
            if (existing.Count < 5)
            {
                _persist.Add(newScore);
                return;
            }

            // Hvis der er 5 eller flere, tjek om den nye score er bedre end den dårligste
            var worstScore = existing[existing.Count - 1].Score;

            if (newScore.Score > worstScore)
            {
                // Den nye score kvalificerer - slet den dårligste og tilføj den nye
                var worstEntry = existing[existing.Count - 1];
                _persist.Delete(worstEntry.Id);
                _persist.Add(newScore);
            }
            // Hvis newScore <= worstScore, gør ingenting (kvalificerer ikke til top 5)
        }

        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }
    }
}
