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

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToPage("/Index");

            // ... resten af din eksisterende kode
           
            Reviews = _reviewService.GetAll();
            return Page();
        }

        public IActionResult OnPostSlet()
        {
            _reviewService.Delete(SletId);
            return RedirectToPage();
        }
    }
}
