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
        private readonly ReviewService _reviewService;


        // Hardkodet ID matcher seed-data i AppDbContext – skal ikke ændres medmindre databasen nulstilles
        public static readonly Guid AppelsinHopGameId =
            Guid.Parse("a1b2c3d4-e5f6-4789-a012-3456789abcde");

        public AppelsinHopModel(HighScoreService highScoreService, GameService gameService, ReviewService reviewService)
        {
            _highScoreService = highScoreService;
            _gameService = gameService;
            _reviewService = reviewService;
        }

        [BindProperty] public string PlayerName { get; set; } = string.Empty;
        [BindProperty] public int Score { get; set; }
        [BindProperty] public string NewReviewText { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = new();

        public int TopScore { get; set; }
        public List<Domain.Models.HighScore> TopHighscores { get; set; } = new();
        public bool IsLoggedIn { get; set; }
        public string SessionUserName { get; set; } = string.Empty;

        public void OnGet()
        {
            SessionUserName = HttpContext.Session.GetString("UserName") ?? string.Empty;
            IsLoggedIn = !string.IsNullOrEmpty(SessionUserName);
            LoadHighscores();
            LoadReviews();
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

        private void LoadReviews()
        {
            Reviews = _reviewService.GetAll()
                .Where(r => r.GameId == AppelsinHopGameId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public IActionResult OnPostSubmitReview()
        {
            var sessionUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(sessionUserId))
            {
                return RedirectToPage("/UserSides/UserLogIn");
            }

            if (string.IsNullOrWhiteSpace(NewReviewText))
            {
                SessionUserName = HttpContext.Session.GetString("UserName") ?? string.Empty;
                IsLoggedIn = !string.IsNullOrEmpty(SessionUserName);

                LoadHighscores();
                LoadReviews();
                return Page();
            }

            var nyAnmeldelse = new Review
            {
                Text = NewReviewText,
                GameId = AppelsinHopGameId,
                UserId = Guid.Parse(sessionUserId)
            };

            _reviewService.Create(nyAnmeldelse);

            return RedirectToPage("/Game/AppelsinHop");
        }
    }
}