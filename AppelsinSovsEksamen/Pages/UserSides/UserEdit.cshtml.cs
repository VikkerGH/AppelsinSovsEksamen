using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly Domain.Services.UserService _userService;

        public IndexModel(Domain.Services.UserService userService)
        {
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Domain.Models.User? CurrentUser { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                CurrentUser = _userService.GetById(Id);
                return Page();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}