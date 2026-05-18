using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Services;

namespace AppelsinSovsEksamen.Pages.AdminGame
{
    public class GameCreateModel : PageModel
    {
        private readonly GameService _gameService;

        public GameCreateModel(GameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty]
        public Domain.Models.Game NewGame { get; set; } = new Domain.Models.Game();

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var alleSpil = _gameService.GetAll();

            bool spilFindes = alleSpil.Any(g => g.Name.ToLower() == NewGame.Name.ToLower());

            if (spilFindes)
            {
                ModelState.AddModelError("NewGame.Name", "Der findes allerede et spil med dette navn.");
                return Page();
            }

            _gameService.Create(NewGame.Name, NewGame.Description);

            return RedirectToPage("./GameIndex");
        }
    }
}