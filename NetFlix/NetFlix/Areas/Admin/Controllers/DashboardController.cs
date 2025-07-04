using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.ViewModels;

namespace NetFlix.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;

        public DashboardController(IMovieService movieService, IActorService actorService)
        {
            _movieService = movieService;
            _actorService = actorService;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();  // GetMovieVm tipində
            var actors = await _actorService.GetAllActorsAsync();  // GetActorVm tipində

            var vm = new DashboardVm
            {
                Movies = movies,
                Actors = actors
            };

            return View(vm);
        }
    }
}
