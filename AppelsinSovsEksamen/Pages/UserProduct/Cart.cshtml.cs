using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using System.Text.Json;

namespace AppelsinSovsEksamen.Pages.UserProduct.Cart
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(i => i.Price * i.Quantity);

        [BindProperty]
        public Guid RemoveProductId { get; set; }

        public void OnGet()
        {
            CartItems = GetCartFromSession();
        }

        public IActionResult OnPostRemove()
        {
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(i => i.ProductId == RemoveProductId);

            if (item != null)
            {
                if (item.Quantity > 1) item.Quantity--;
                else cart.Remove(item);
            }

            SaveCartToSession(cart);
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            HttpContext.Session.Remove("Cart");
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
