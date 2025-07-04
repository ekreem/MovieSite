using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting; // IWebHostEnvironment üçün
using Microsoft.AspNetCore.Mvc;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NetFlix.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")] // Admin sahəsi üçün
    [Authorize(Roles = "Admin")]
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;
        private readonly IMovieService _movieService;
        private readonly IWebHostEnvironment _environment;

        public ActorsController(IActorService actorService, IMovieService movieService, IWebHostEnvironment environment)
        {
            _actorService = actorService;
            _movieService = movieService;
            _environment = environment;
        }

        // GET: /Admin/Actors
        public async Task<IActionResult> Index()
        {
            var actors = await _actorService.GetAllActorsAsync();
            return View(actors);
        }

        // GET: /Admin/Actors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor == null)
                return NotFound();
            return View(actor);
        }

        // GET: /Admin/Actors/Create
        public async Task<IActionResult> Create()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            ViewBag.Movies = movies.Select(m => new { m.Id, m.Title }).ToList();
            return View(new CreateActorVm());
        }

        // POST: /Admin/Actors/Create
        [HttpPost]

        public async Task<IActionResult> Create(CreateActorVm actorVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _actorService.CreateActorAsync(actorVm, _environment.WebRootPath); // WebRootPath ötürülür
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating actor: {ex.Message}");
                }
            }

            var movies = await _movieService.GetAllMoviesAsync();
            ViewBag.Movies = movies.Select(m => new { m.Id, m.Title }).ToList();
            return View(actorVm);
        }

        // GET: /Admin/Actors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor == null)
                return NotFound();

            var updateActorVm = new UpdateActorVm
            {
                Id = actor.Id,
                FullName = actor.FullName,
                BirthDate = actor.BirthDate,
                BirthPlace = actor.BirthPlace,
                PhotoFileName = actor.PhotoFileName,
                MovieIds = actor.Movies.Select(m => m.Id).ToList()
            };

            var movies = await _movieService.GetAllMoviesAsync();
            ViewBag.Movies = movies.Select(m => new { m.Id, m.Title }).ToList();
            return View(updateActorVm);
        }

        // POST: /Admin/Actors/Edit/5
        [HttpPost]

        public async Task<IActionResult> Edit(int id, UpdateActorVm actorVm)
        {
            if (id != actorVm.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _actorService.UpdateActorAsync(actorVm, _environment.WebRootPath); // WebRootPath ötürülür
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating actor: {ex.Message}");
                }
            }

            var movies = await _movieService.GetAllMoviesAsync();
            ViewBag.Movies = movies.Select(m => new { m.Id, m.Title }).ToList();
            return View(actorVm);
        }

        // GET: /Admin/Actors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorService.GetActorByIdAsync(id);
            if (actor == null)
                return NotFound();
            return RedirectToAction("Index","Actors");
        }

        // POST: /Admin/Actors/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _actorService.DeleteActorAsync(id, _environment.WebRootPath); // WebRootPath ötürülür
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting actor: {ex.Message}");
                var actor = await _actorService.GetActorByIdAsync(id);
                if (actor == null)
                    return NotFound();
                return View(actor);
            }
        }
    }
}