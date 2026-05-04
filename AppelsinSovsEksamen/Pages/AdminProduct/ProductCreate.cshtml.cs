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
        private readonly IRepository<Domain.Models.Kategori> _kategoriRepo;

        public ProductCreateModel (ProductService productService, IRepository<Domain.Models.Kategori> kategoriRepo)
        {
            _productService = productService;
            _kategoriRepo = kategoriRepo;
        }

        [BindProperty]
        public Domain.Models.Product NewProduct { get; set; } = new Domain.Models.Product();

        public IEnumerable<Kategori> Kategorier { get; set; } = new List<Kategori>();

        public void OnGet()
        {
            Kategorier = _kategoriRepo.GetAll();
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
