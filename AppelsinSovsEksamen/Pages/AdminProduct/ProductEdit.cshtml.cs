using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using Domain.Persistence;
using System;
using System.Collections.Generic;

namespace AppelsinSovsEksamen.Pages.AdminProduct
{
    public class ProductEditModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly IRepository<Kategori> _kategoriRepo;

        public ProductEditModel(ProductService productService, IRepository<Kategori> kategoriRepo)
        {
            _productService = productService;
            _kategoriRepo = kategoriRepo;
        }

        [BindProperty]
        public Domain.Models.Product ProductToEdit { get; set; } = new Domain.Models.Product();

        public IEnumerable<Kategori> Kategorier { get; set; } = new List<Kategori>();

        public IActionResult OnGet(Guid id)
        {
            Kategorier = _kategoriRepo.GetAll();

           
            var product = _productService.GetById(id);

            if (product == null)
            {
                return RedirectToPage("./ProductIndex");
            }

            ProductToEdit = product;

            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost(Guid id)
        {
            ModelState.Remove("ProductToEdit.Kategori");

            if (!ModelState.IsValid)
            {
                Kategorier = _kategoriRepo.GetAll();
                return Page();
            }

            var existingProduct = _productService.GetById(id);

            if (existingProduct == null)
            {
                return RedirectToPage("./ProductIndex");
            }

            existingProduct.Name = ProductToEdit.Name;
            existingProduct.Price = ProductToEdit.Price;
            existingProduct.ImageUrl = ProductToEdit.ImageUrl;
            existingProduct.KategoriId = ProductToEdit.KategoriId;

            _productService.Update(existingProduct);

            return RedirectToPage("./ProductIndex");
        }
    }
}