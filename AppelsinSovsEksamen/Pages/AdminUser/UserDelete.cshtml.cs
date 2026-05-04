using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.AdminUser
{
    public class UserDeleteModel : PageModel
    {
        private readonly Domain.Services.UserService _userService;

        public UserDeleteModel(Domain.Services.UserService userService)
        {
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public Domain.Models.User? UserToDelete { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                UserToDelete = _userService.GetById(Id);
                return Page();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                _userService.Delete(Id);
                return RedirectToPage("UserIndex");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}