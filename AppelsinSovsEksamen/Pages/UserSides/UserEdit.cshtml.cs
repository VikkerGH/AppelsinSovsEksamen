using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace AppelsinSovsEksamen.Pages.User
{
    public class UserEditModel : PageModel
    {
        private readonly UserService _userService;
        private readonly HighScoreService _highScoreService;
        private readonly GameService _gameService;

        public UserEditModel(Domain.Services.UserService userService, HighScoreService highScoreService, GameService gameService)
        {
            _userService = userService;
            _highScoreService = highScoreService;
            _gameService = gameService;
        }

        public Domain.Models.User? CurrentUser { get; set; }
        public Dictionary<string, List<Domain.Models.HighScore>> TopScoresByGame { get; set; } = new();


        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Navn er påkrævet")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [StringLength(100, MinimumLength = 6,
     ErrorMessage = "Kodeord skal være mindst 6 tegn")]
        public string? Password { get; set; }

        public IActionResult OnGet()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return RedirectToPage("/UserSides/UserLogIn");

            try
            {
                CurrentUser = _userService.GetById(userId);
                Name = CurrentUser.Name;

                var games = _gameService.GetAll();
                var allScores = _highScoreService.GetAll()
                    .Where(hs => hs.UserId == userId)
                    .ToList();

                foreach (var game in games)
                {
                    var top3 = allScores
                        .Where(hs => hs.GameId == game.Id)
                        .OrderByDescending(hs => hs.Score)
                        .Take(3)
                        .ToList();

                    if (top3.Any())
                        TopScoresByGame[game.Name] = top3;
                }

                return Page();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPost()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToPage("/UserSides/UserLogIn");
            }

            if (!ModelState.IsValid)
                return Page();

            _userService.Edit(userId, Name, Password);
            HttpContext.Session.SetString("UserName", Name);
            TempData["Success"] = "Dine oplysninger er blevet opdateret.";
            return RedirectToPage("/UserSides/UserEdit");
        }
    }
}