using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Services;
using System;

namespace AppelsinSovsEksamen.Pages.AdminGame
{
    public class GameEditModel : PageModel
    {
        private readonly GameService _gameService;

        public GameEditModel(GameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public Domain.Models.Game GameToEdit { get; set; } = new Domain.Models.Game();

        public IActionResult OnGet()
        {
            try
            {
                var game = _gameService.GetById(Id);
                GameToEdit = game;
                return Page();
            }
            catch (ArgumentException)
            {
                return RedirectToPage("./GameIndex");
            }
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("GameToEdit.Kategori");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _gameService.Edit(Id, GameToEdit.Name, GameToEdit.Description);
                return RedirectToPage("./GameIndex");
            }
            catch (ArgumentException)
            {
                return RedirectToPage("./GameIndex");
            }
        }
    }
}