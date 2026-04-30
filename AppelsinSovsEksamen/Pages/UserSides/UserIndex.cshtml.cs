using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppelsinSovsEksamen.Pages.User
{
        public class LoginModel : PageModel
        {
            private readonly IRepository<Domain.Models.User> _userRepository;

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

            public void OnGet()
            {
            
            }

            public IActionResult OnPost()
            {
                // Tjek at felter er udfyldt
                if (!ModelState.IsValid)
                {
                    return Page();
                }

            
                // Send til forsiden
                return RedirectToPage("/Index");
            }
        }
}
