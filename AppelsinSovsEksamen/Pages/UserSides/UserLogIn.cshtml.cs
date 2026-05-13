using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppelsinSovsEksamen.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<Domain.Models.User> _userRepository;

        // Indsæt dit admin Guid her fra databasen
        private static readonly Guid AdminId = Guid.Parse("INDSÆT-ADMIN-GUID-HER");

        public LoginModel(IRepository<Domain.Models.User> userRepository)
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

            // Gem session
            HttpContext.Session.SetString("UserId", bruger.Id.ToString());
            HttpContext.Session.SetString("UserName", bruger.Name);

            // Tjek om brugeren er admin ud fra Id
            bool isAdmin = bruger.Id == AdminId;
            HttpContext.Session.SetString("IsAdmin", isAdmin.ToString().ToLower());

            Response.Cookies.Append(".AppelsinSovs.Session",
                Request.Cookies[".AppelsinSovs.Session"] ?? "",
                new CookieOptions { Expires = null });

            // Send admin til admin-panel, normale brugere til forsiden
            if (isAdmin)
                return RedirectToPage("/AdminIndex");

            return RedirectToPage("/Index");
        }
    }
}