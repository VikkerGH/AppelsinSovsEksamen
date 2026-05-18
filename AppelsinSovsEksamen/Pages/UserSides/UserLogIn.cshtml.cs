using Domain.Persistence;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppelsinSovsEksamen.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userRepository;

        public LoginModel(UserService userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        [MinLength(3, ErrorMessage = "Brugernavn skal være mindst 3 tegn")]
        public string InputName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        public string InputPassword { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var bruger = _userRepository.GetAll()
                .FirstOrDefault(u => u.Name.ToLower() == InputName.ToLower());

            if (bruger == null)
            {
                ErrorMessage = "Forkert brugernavn eller adgangskode.";
                return Page();
            }

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Domain.Models.User>();
            var result = hasher.VerifyHashedPassword(bruger, bruger.Password, InputPassword);

            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                ErrorMessage = "Forkert brugernavn eller adgangskode.";
                return Page();
            }

            HttpContext.Session.SetString("UserId", bruger.Id.ToString());
            HttpContext.Session.SetString("UserName", bruger.Name);
            HttpContext.Session.SetString("IsAdmin", bruger.IsAdmin.ToString().ToLower());

            Response.Cookies.Append(".AppelsinSovs.Session",
                Request.Cookies[".AppelsinSovs.Session"] ?? "",
                new CookieOptions { Expires = null });

            if (bruger.IsAdmin)
                return RedirectToPage("/AdminUser/AdminIndex");

            return RedirectToPage("/Index");
        }
    }
}