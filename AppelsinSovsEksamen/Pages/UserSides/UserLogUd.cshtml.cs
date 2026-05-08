using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.UserSides
{
    public class UserLogUdModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/    Index");
        }
    }
}
