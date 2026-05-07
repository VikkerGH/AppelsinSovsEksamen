using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using System;

namespace AppelsinSovsEksamen.Pages.AdminProduct
{
    public class ProductDeleteModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductDeleteModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Domain.Models.Product ProductToDelete { get; set; } = new Domain.Models.Product();

        public IActionResult OnGet(Guid id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return RedirectToPage("./ProductIndex");
            }

            ProductToDelete = product;
            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            _productService.Delete(id);

            return RedirectToPage("./ProductIndex");
        }
    }
}