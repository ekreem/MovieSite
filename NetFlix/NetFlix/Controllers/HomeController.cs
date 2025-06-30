using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
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
            return View(filmler);
        }
    }
}
