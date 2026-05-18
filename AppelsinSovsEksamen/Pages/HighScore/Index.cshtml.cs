using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.HighScore
{
    public class IndexModel : PageModel
    {
        private readonly HighScoreService _highScoreService;
        private readonly GameService _gameService;

        public IndexModel(HighScoreService highScoreService, GameService gameService)
        {
            _highScoreService = highScoreService;
            _gameService = gameService;
        }

        public Dictionary<string, List<Domain.Models.HighScore>> ScorePerSpil { get; set; } = new();

        public void OnGet()
        {
            var alleSpil = _gameService.GetAll(); // Hent alle spil først, så vi kan matche scores til spilnavne
            var alleScores = _highScoreService.GetAll();// Hent alle scores

            foreach (var spil in alleSpil) // Gå igennem hvert spil
            {
                List<Domain.Models.HighScore> scoresForSpil = alleScores
                    .Where(h => h.GameId == spil.Id)// Filtrer scores for det aktuelle spil
                    .OrderByDescending(h => h.Score)// Sorter scores i faldende rækkefølge
                    .Take(5)// Tag de top 5 scores
                    .ToList();// Konverter til liste

                ScorePerSpil[spil.Name] = scoresForSpil;// Gem scores i dictionary med spilnavn som nøgle
            }
        }
    }
}