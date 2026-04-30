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

        public IEnumerable<Domain.Models.HighScore> HighScores { get; set; } = [];
        public IEnumerable<Domain.Models.Game> Games { get; set; } = [];

        public void OnGet()
        {
            HighScores = _highScoreService.GetAll().OrderByDescending(h => h.Score);
            Games = _gameService.GetAll();
        }
    }
}
