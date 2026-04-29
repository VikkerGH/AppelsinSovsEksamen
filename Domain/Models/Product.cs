using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        public Product() { }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } 
        public Kategori Kategori { get; set; }
    }
}
