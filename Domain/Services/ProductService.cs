using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
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
        public Product Create(string name, decimal price)
        {
            var product = new Product();
            _persist.Add(product);
            return product;
        }
        public void Edit(Guid id, string newName, decimal newPrice)
        {
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
