using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    // Bruges til at gemme kurv-data i session som JSON – ikke i databasen
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
    }
}
