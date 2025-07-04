using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.BLL.Services.Concretes;
using System.Threading.Tasks;

namespace NetFlix.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var filmler = await movieService.GetAllMoviesAsync();
            var inOrder= filmler.OrderByDescending(x=>x.Year).ToList();
            ViewBag.InOrder = inOrder;
            return View(filmler);
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var result = await movieService.SearchByTitleAsync(query);
            return View(result); // bu Search.cshtml-i açacaq
        }

    }
}
