using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;
using System.Text.Json;

namespace AppelsinSovsEksamen.Pages.UserProduct
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<Domain.Models.Product> Products { get; set; } = new List<Domain.Models.Product>();
        public string? ConfirmMessage { get; set; }

        [BindProperty]
        public Guid ProductId { get; set; }

        public void OnGet()
        {
            Products = _productService.GetAll();
            ConfirmMessage = TempData["CartConfirm"] as string;
        }

        public IActionResult OnPost()
        {
            var product = _productService.GetById(ProductId);
            var cart = GetCartFromSession();

            var existing = cart.FirstOrDefault(i => i.ProductId == ProductId);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = 1
                });
            }

            SaveCartToSession(cart);
            TempData["CartConfirm"] = $"✅ \"{product.Name}\" er tilføjet til kurven!";
            return RedirectToPage();
        }

        private List<CartItem> GetCartFromSession()
        {
            var json = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(json)) return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
    }
}
