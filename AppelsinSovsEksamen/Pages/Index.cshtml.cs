using Domain.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppelsinSovsEksamen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Domain.Models.Game> _gameRepository;
        private readonly IRepository<Domain.Models.Kategori> _kategoriRepository;

        // Listen af spil der vises på forsiden (kan være filtreret)
        public IEnumerable<Domain.Models.Game> Games { get; set; } = new List<Domain.Models.Game>();

        // Bruges til søgning – SupportsGet = true så værdien følger med i URL'ens query string
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        // Bruges til kategori-filter – samme princip som SearchString
        [BindProperty(SupportsGet = true)]
        public string? SelectedKategori { get; set; }

        // Alle kategorier til dropdown-menuen
        public IEnumerable<Domain.Models.Kategori> Kategorier { get; set; } = new List<Domain.Models.Kategori>();

        public IndexModel(IRepository<Domain.Models.Game> gameRepository, IRepository<Domain.Models.Kategori> kategoriRepository)
        {
            _gameRepository = gameRepository;
            _kategoriRepository = kategoriRepository;
        }

        public void OnGet()
        {
            Kategorier = _kategoriRepository.GetAll();

            var games = _gameRepository.GetAll();

            // Filtrer på navn hvis brugeren har skrevet noget i søgefeltet (SN2-17)
            if (!string.IsNullOrWhiteSpace(SearchString))
                games = games.Where(g => g.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            // Filtrer på kategori hvis brugeren har valgt en i dropdown (SN2-22)
            if (!string.IsNullOrWhiteSpace(SelectedKategori))
                games = games.Where(g => g.Kategori != null && g.Kategori.Name == SelectedKategori);

            Games = games.ToList();
        }
    }
}
