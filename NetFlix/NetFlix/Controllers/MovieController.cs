using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;

namespace NetFlix.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET: /Movie/Details/5
        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}
