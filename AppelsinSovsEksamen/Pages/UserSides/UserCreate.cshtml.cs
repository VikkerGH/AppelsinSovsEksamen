using Domain.Persistence;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppelsinSovsEksamen.Pages.User
{
    public class CreateUserModel : PageModel
    {
        private readonly UserService _userRepository;

        public CreateUserModel(UserService userRepository)
        {
            _userRepository = userRepository;
        }

        // Binding af formular-input
        [BindProperty]
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        [MinLength(3, ErrorMessage = "Brugernavn skal være mindst 3 tegn")]
        public string InputName { get; set; } = string.Empty;

        // Adgangskode skal være mindst 6 tegn for at være sikker nok
        [BindProperty]
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [MinLength(6, ErrorMessage = "Adgangskode skal være mindst 6 tegn")]
        public string InputPassword { get; set; } = string.Empty;

        // Fejlbesked der vises i formularen
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // Siden indlæses - intet skal ske her
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var allebrugere = _userRepository.GetAll();
            bool brugernavnTaget = allebrugere.Any(u => u.Name.ToLower() == InputName.ToLower());

            if (brugernavnTaget)
            {
                ErrorMessage = "Brugernavnet er allerede taget. Vælg et andet.";
                return Page();
            }

            var nyBruger = _userRepository.Create(InputName, InputPassword);

            HttpContext.Session.SetString("UserId", nyBruger.Id.ToString());
            HttpContext.Session.SetString("UserName", nyBruger.Name);
            HttpContext.Session.SetString("IsAdmin", "false");

            return RedirectToPage("/Index");
        }
    }
}
