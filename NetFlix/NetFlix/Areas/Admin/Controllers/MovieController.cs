using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.ViewModels;
using System.Threading.Tasks;

namespace NetFlix.Web.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: /Movie
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
        }

        // GET: /Movie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: /Movie/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateMovieVm());
        }

        // POST: /Movie/Create
        [HttpPost]

        public async Task<IActionResult> Create(CreateMovieVm movieVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.CreateMovie(movieVm);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(movieVm);
        }

        // GET: /Movie/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var updateMovieVm = new UpdateMovieVm
            {
                Id = id,
                Title = movie.Title,
                Category = movie.Category,
                Description = movie.Description,
                Director = movie.Director,
                Year = movie.Year,
                DurationMinutes = movie.DurationMinutes,
                ImdbRating = movie.ImdbRating,
                TrailerUrl = movie.TrailerUrl,
                Quality = movie.Quality,
                ActorsIds = movie.Actors?.Select(a => a.Id).ToList() ?? new List<int>()
            };

            return View(updateMovieVm);
        }

        // POST: /Movie/Edit/5
        [HttpPost]

        public async Task<IActionResult> Edit(UpdateMovieVm movieVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _movieService.UpdateMovie(movieVm);
                    await _movieService.SaveAllChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(movieVm);
        }

        // GET: /Movie/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: /Movie/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
                await _movieService.SaveAllChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var movie = await _movieService.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return View(movie);
            }
        }
    }
}   