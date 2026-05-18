using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using System.Collections.Generic;

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

        public void OnGet()
        {
            Games = _gameService.GetAll();
        }
    }
}