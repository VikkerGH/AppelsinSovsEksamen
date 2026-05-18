using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Services;
using Domain.Models;
using System;

namespace AppelsinSovsEksamen.Pages.AdminGame
{
    public class GameDeleteModel : PageModel
    {
        private readonly GameService _gameService;

        public GameDeleteModel(GameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Domain.Models.Game? GameToDelete { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            try
            {
                GameToDelete = _gameService.GetById(Id);
                return Page();
            }
            catch (ArgumentException)
            {
                return RedirectToPage("./GameIndex");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                _gameService.Delete(Id);
                return RedirectToPage("./GameIndex");
            }
            catch (ArgumentException)
            {
                return RedirectToPage("./GameIndex");
            }
        }
    }
}