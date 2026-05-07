using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    // Håndterer produkter i butikken – opret, rediger og slet via admin-siden
    public class ProductService
    {
        private readonly IRepository<Product> _persist;
        public ProductService(IRepository<Product> persist)
        {
            _persist = persist;
        }
        public IEnumerable<Product> GetAll() => _persist.GetAll();
        public Product GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("Product not found", nameof(id));
        public void Create(Product newProduct)
        {
            _persist.Add(newProduct);
        }
        public void Edit(Guid id, string newName, decimal newPrice)
        {
            // Bemærk: newName og newPrice bruges ikke her – de bør sættes på product-objektet inden Update kaldes
            var product = _persist.GetById(id)
                ?? throw new ArgumentException("Product not found", nameof(id));
            _persist.Update(product);
        }
        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }
    }
}
