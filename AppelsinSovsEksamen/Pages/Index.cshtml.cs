using Domain.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Domain.Models.Game> _gameRepository;

        public IEnumerable<Domain.Models.Game> Games { get; set; } = new List<Domain.Models.Game>();

        public IndexModel(IRepository<Domain.Models.Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void OnGet()
        {
            Games = _gameRepository.GetAll();
        }
    }
}
