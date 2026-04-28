using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        public Product() { }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } 
        public Kategori Kategori { get; set; }
    }
}
