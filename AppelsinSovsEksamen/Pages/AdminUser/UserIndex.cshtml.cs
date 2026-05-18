using Domain.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages.AdminUser
{
    public class UserIndexModel : PageModel
    {
        private readonly UserService _userService;

        public UserIndexModel(UserService userService)
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