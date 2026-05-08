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
            var allForGame = _persist.GetAll()
                .Where(hs => hs.GameId == newScore.GameId)
                .ToList();

            // Global top 5
            var globalScores = allForGame
                .OrderByDescending(hs => hs.Score)
                .ToList();

            if (globalScores.Count < 5)
            {
                _persist.Add(newScore);
            }
            else
            {
                var worstGlobal = globalScores.Last();
                if (newScore.Score > worstGlobal.Score)
                {
                    _persist.Delete(worstGlobal.Id);
                    _persist.Add(newScore);
                }
            }

            // Personlig top 3 — kun for loggede brugere
            if (newScore.UserId.HasValue)
            {
                var userScores = allForGame
                    .Where(hs => hs.UserId == newScore.UserId)
                    .OrderByDescending(hs => hs.Score)
                    .ToList();

                if (userScores.Count < 3)
                {
                    _persist.Add(newScore);
                }
                else
                {
                    var worstUserScore = userScores.Last();
                    if (newScore.Score > worstUserScore.Score)
                    {
                        _persist.Delete(worstUserScore.Id);
                        _persist.Add(newScore);
                    }
                }
            }
        }

        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }
    }
}
