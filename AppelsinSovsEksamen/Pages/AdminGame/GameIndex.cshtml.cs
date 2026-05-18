using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AppelsinSovsEksamen.Pages.AdminGame
{
    public class GameIndexModel : PageModel
    {
        private readonly GameService _gameService;

        public GameIndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerable<Domain.Models.Game> Games { get; set; } = new List<Domain.Models.Game>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            Games = _gameService.GetAll();

            return Page();
        }
    }
}