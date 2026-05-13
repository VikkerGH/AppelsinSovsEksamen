using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using System.Collections.Generic;

namespace AppelsinSovsEksamen.Pages.AdminProduct
{
    public class ProductIndexModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductIndexModel(ProductService productService)
        { 
            _productService = productService;
        }

        public IEnumerable<Domain.Models.Product> Products { get; set; } = new List<Domain.Models.Product>();
        public IActionResult OnGet()
        {
            Products = _productService.GetAll();
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            return Page();
        }
    }
}
