using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
            {
                return RedirectToPage("/UserSides/UserLogIn");
            }

            try
            {
                CurrentUser = _userService.GetById(userId);

                // Udfyld felterne
                Name = CurrentUser.Name;

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