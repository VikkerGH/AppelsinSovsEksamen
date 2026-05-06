using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.User
{
    public class UserEditModel : PageModel
    {
        private readonly Domain.Services.UserService _userService;

        public UserEditModel(Domain.Services.UserService userService)
        {
            _userService = userService;
        }

        public Domain.Models.User? CurrentUser { get; set; }

        public IActionResult OnGet()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToPage("/UserSides/UserLogIn");
            }

            try
            {
                CurrentUser = _userService.GetById(userId);
                return Page();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}