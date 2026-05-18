using Domain.Persistence;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.AdminUser
{
    public class AdminIndexModel : PageModel
    {
        private readonly UserService _userRepo;
        private readonly ProductService _productRepo;

        public int UserCount { get; set; }
        public int ProductCount { get; set; }
        public int AnmeldelseCount { get; set; }
    


        public AdminIndexModel(
            UserService userRepo,
            ProductService productRepo)
        {
            _userRepo = userRepo;
            _productRepo = productRepo;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            UserCount = _userRepo.GetAll().Count();
            ProductCount = _productRepo.GetAll().Count();
            AnmeldelseCount = 0;

            return Page();
        }
    }
}