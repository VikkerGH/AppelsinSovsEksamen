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
        public void OnGet()
        {
            Products = _productService.GetAll();
        }
    }
}
