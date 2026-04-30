using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.AdminUser
{
    public class UserIndexModel : PageModel
    {
        private readonly Domain.Services.UserService _userService;

        public UserIndexModel(Domain.Services.UserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<Domain.Models.User> Users { get; set; } = [];

        public void OnGet()
        {
            Users = _userService.GetAll();
        }
    }
}