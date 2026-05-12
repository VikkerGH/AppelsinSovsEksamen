using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.Services;

namespace AppelsinSovsEksamen.Pages.AdminReview
{
    public class ReviewIndexModel : PageModel
    {
        private readonly ReviewService _reviewService;

        public ReviewIndexModel(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();

        [BindProperty]
        public Guid SletId { get; set; }

        public void OnGet()
        {
            Reviews = _reviewService.GetAll();
        }

        public IActionResult OnPostSlet()
        {
            _reviewService.Delete(SletId);
            return RedirectToPage();
        }
    }
}
