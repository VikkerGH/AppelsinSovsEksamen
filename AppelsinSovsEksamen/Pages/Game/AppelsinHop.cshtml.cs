using Domain.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.Game
{
    public class AppelsinHopModel : PageModel
    {
        private readonly HighScoreService _highScoreService;
        private readonly GameService _gameService;

        // Fast GameId for "Appelsin Hop" (matcher seed-data i AppDbContext)
        public static readonly Guid AppelsinHopGameId =
            Guid.Parse("a1b2c3d4-e5f6-4789-a012-3456789abcde");

        public AppelsinHopModel(HighScoreService highScoreService, GameService gameService)
        {
            _highScoreService = highScoreService;
            _gameService = gameService;
        }

        [BindProperty]
        public string PlayerName { get; set; } = string.Empty;

        [BindProperty]
        public int Score { get; set; }

        public int TopScore { get; set; }
        public List<Domain.Models.HighScore> TopHighscores { get; set; } = new();

        public void OnGet()
        {
            LoadHighscores();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (Score <= 0)
                {
                    return Page();
                }

                var hs = new Domain.Models.HighScore
                {
                    Score = Score,
                    PlayerName = string.IsNullOrWhiteSpace(PlayerName) ? "Anonym" : PlayerName,
                    GameId = AppelsinHopGameId
                };

                _highScoreService.AddHighScore(hs);

                // Redirect til samme side for at undgå form resubmission
                return RedirectToPage("/Game/AppelsinHop");
            }
            catch (Exception ex)
            {
                // Log fejlen (i produktion ville du bruge ILogger)
                Console.WriteLine($"Fejl ved gem af highscore: {ex.Message}");

                // Genindlæs siden med data
                LoadHighscores();
                return Page();
            }
        }

        private void LoadHighscores()
        {
            try
            {
                var allScores = _highScoreService.GetAll()
                    .Where(h => h.GameId == AppelsinHopGameId)
                    .OrderByDescending(h => h.Score)
                    .ToList();

                TopScore = allScores.Any() ? allScores.First().Score : 0;
                TopHighscores = allScores.Take(5).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved indlæsning af highscores: {ex.Message}");
                TopScore = 0;
                TopHighscores = new List<Domain.Models.HighScore>();
            }
        }
    }
}
