using Domain.Models;
using Domain.Persistence;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.AdminProduct
{
    public class ProductCreateModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly IRepository<Kategori> _kategoriRepo;

        public ProductCreateModel (ProductService productService, IRepository<Kategori> kategoriRepo)
        {
            _productService = productService;
            _kategoriRepo = kategoriRepo;
        }

        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        public IEnumerable<Kategori> Kategorier { get; set; } = new List<Kategori>();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");
            
            Kategorier = _kategoriRepo.GetAll();

            return Page();
        }

        public IActionResult OnPost()
        { 
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.Create(NewProduct);

            return RedirectToPage("./ProductIndex");
        
        }
    }
}
