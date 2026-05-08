using Domain.Models;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ReviewService
    {
        private readonly IRepository<Review> _persist;

        public ReviewService(IRepository<Review> persist)
        {
            _persist = persist;
        }

        public IEnumerable<Review> GetAll() => _persist.GetAll();

        public Review GetById(Guid id) => _persist.GetById(id)
            ?? throw new ArgumentException("Review not found", nameof(id));

        public void Create(Review newReview)
        {
            _persist.Add(newReview);
        }
        public void Update(Review updatedReview)
        {
            _persist.Update(updatedReview);
        }
        public void Delete(Guid id)
        {
            _persist.Delete(id);
        }
    }
}
