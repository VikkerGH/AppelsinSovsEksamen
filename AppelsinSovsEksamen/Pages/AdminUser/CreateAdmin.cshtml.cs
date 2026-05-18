using Domain.Persistence;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppelsinSovsEksamen.Pages.AdminUser
{
    public class CreateAdminModel : PageModel
    {
        private readonly UserService _userRepository;

        public CreateAdminModel(UserService userRepository)
        {
            _userRepository = userRepository;
        }

        // Binding af formular-input
        [BindProperty]
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        [MinLength(3, ErrorMessage = "Brugernavn skal være mindst 3 tegn")]
        public string InputName { get; set; } = string.Empty;

        [BindProperty]
        public bool IsAdmin { get; set; } = false;

        // Adgangskode skal være mindst 6 tegn for at være sikker nok
        [BindProperty]
        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [MinLength(6, ErrorMessage = "Adgangskode skal være mindst 6 tegn")]
        public string InputPassword { get; set; } = string.Empty;

        // Fejlbesked der vises i formularen
        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
           
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");
            
            return Page();
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

            var nyBruger = _userRepository.CreateAdmin(InputName, InputPassword, IsAdmin);

            HttpContext.Session.SetString("UserId", nyBruger.Id.ToString());
            HttpContext.Session.SetString("UserName", nyBruger.Name);
            HttpContext.Session.SetString("IsAdmin", nyBruger.IsAdmin.ToString().ToLower());

            if (nyBruger.IsAdmin)
                return RedirectToPage("/AdminUser/AdminIndex");

            return RedirectToPage("/Index");
        }
    }
}
