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

        public static readonly Guid AppelsinHopGameId =
            Guid.Parse("a1b2c3d4-e5f6-4789-a012-3456789abcde");

        public AppelsinHopModel(HighScoreService highScoreService, GameService gameService)
        {
            _highScoreService = highScoreService;
            _gameService = gameService;
        }

        [BindProperty] public string PlayerName { get; set; } = string.Empty;
        [BindProperty] public int Score { get; set; }

        public int TopScore { get; set; }
        public List<Domain.Models.HighScore> TopHighscores { get; set; } = new();
        public bool IsLoggedIn { get; set; }
        public string SessionUserName { get; set; } = string.Empty;

        public void OnGet()
        {
            SessionUserName = HttpContext.Session.GetString("UserName") ?? string.Empty;
            IsLoggedIn = !string.IsNullOrEmpty(SessionUserName);
            LoadHighscores();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (Score <= 0) { LoadHighscores(); return Page(); }

                var sessionUserId = HttpContext.Session.GetString("UserId");
                var sessionUserName = HttpContext.Session.GetString("UserName");

                string finalName;
                Guid? userId = null;

                if (!string.IsNullOrEmpty(sessionUserId) && !string.IsNullOrEmpty(sessionUserName))
                {
                    finalName = sessionUserName;
                    userId = Guid.Parse(sessionUserId);
                }
                else
                {
                    finalName = string.IsNullOrWhiteSpace(PlayerName) ? "Anonym" : PlayerName.Trim();
                }

                var hs = new Domain.Models.HighScore
                {
                    Score = Score,
                    PlayerName = finalName,
                    GameId = AppelsinHopGameId,
                    UserId = userId
                };

                _highScoreService.AddHighScore(hs);
                return RedirectToPage("/Game/AppelsinHop");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
                LoadHighscores();
                return Page();
            }
        }

        private void LoadHighscores()
        {
            var allScores = _highScoreService.GetAll()
                .Where(h => h.GameId == AppelsinHopGameId)
                .OrderByDescending(h => h.Score)
                .ToList();

            TopScore = allScores.Any() ? allScores.First().Score : 0;
            TopHighscores = allScores.Take(5).ToList();
        }
    }
}