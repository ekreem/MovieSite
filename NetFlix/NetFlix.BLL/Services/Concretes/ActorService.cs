using Microsoft.AspNetCore.Hosting;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using NetFlix.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Concretes
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _environment;

        public ActorService(IActorRepository actorRepository, IMovieRepository movieRepository, IWebHostEnvironment environment)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _environment = environment;
        }

        public async Task<List<GetActorVm>> GetAllActorsAsync()
        {
            var actors = await _actorRepository.GetAllWithMoviesAsync();
            return actors.Select(a => MapToGetActorVm(a)).ToList();
        }

        public async Task<GetActorVm> GetActorByIdAsync(int id)
        {
            var actor = await _actorRepository.GetByIdWithMoviesAsync(id);
            if (actor == null)
                return null;
            return MapToGetActorVm(actor);
        }

        public async Task CreateActorAsync(CreateActorVm actorVm,string webRootPath)
        {

           
                var movies = await _movieRepository.GetAllWithActorsAsync();
            
         

            
            var selectedMovies = new List<Movie>();
            if (actorVm.MovieIds != null) { 
            selectedMovies = movies.Where(m => actorVm.MovieIds.Contains(m.Id)).ToList();
            }

            

            string fileName = null;
            if (actorVm.PhotoFile != null && actorVm.PhotoFile.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(actorVm.PhotoFile.FileName);
                var path = Path.Combine(_environment.WebRootPath, "Upload", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await actorVm.PhotoFile.CopyToAsync(fileStream);
                }
            }

            var actor = new Actor
            {
                FullName = actorVm.FullName,
                BirthDate = actorVm.BirthDate,
                BirthPlace = actorVm.BirthPlace,
                Imgurl = fileName != null ? "/Upload/" + fileName : string.Empty,
                Movies = selectedMovies
            };

            await _actorRepository.CreateActorAsync(actor);
            await _actorRepository.SaveAllChangesAsync();
        }

        public async Task UpdateActorAsync(UpdateActorVm actorVm, string webRootPath)
        {
            var actor = await _actorRepository.GetByIdWithMoviesAsync(actorVm.Id);
            if (actor == null)
                throw new ArgumentException($"Actor with ID {actorVm.Id} not found.");

            var movies = await _movieRepository.GetAllMovies();
            var selectedMovies = movies.Where(m => actorVm.MovieIds.Contains(m.Id)).ToList();

            if (selectedMovies == null || !selectedMovies.Any())
                throw new ArgumentException("No valid movies selected.");

            string fileName = actor.Imgurl;
            if (actorVm.PhotoFile != null && actorVm.PhotoFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(actor.Imgurl))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, actor.Imgurl.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(actorVm.PhotoFile.FileName);
                var path = Path.Combine(_environment.WebRootPath, "Upload", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await actorVm.PhotoFile.CopyToAsync(fileStream);
                }
            }

            actor.FullName = actorVm.FullName;
            actor.BirthDate = actorVm.BirthDate;
            actor.BirthPlace = actorVm.BirthPlace;
            actor.Imgurl = fileName != null ? "/Upload/" + fileName : actor.Imgurl;
            actor.Movies = selectedMovies;

            await _actorRepository.UpdateActorAsync(actor);
            await _actorRepository.SaveAllChangesAsync();
        }

        public async Task DeleteActorAsync(int id, string webRootPath)
        {
            var actor = await _actorRepository.GetByIdWithMoviesAsync(id);
            if (actor == null)
                throw new ArgumentException($"Actor with ID {id} not found.");

            if (!string.IsNullOrEmpty(actor.Imgurl))
            {
                var filePath = Path.Combine(_environment.WebRootPath, actor.Imgurl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _actorRepository.DeleteActorAsync(id);
            await _actorRepository.SaveAllChangesAsync();
        }

        private GetActorVm MapToGetActorVm(Actor actor)
        {
            return new GetActorVm
            {
                Id = actor.Id,
                FullName = actor.FullName,
                BirthDate = actor.BirthDate,
                BirthPlace = actor.BirthPlace,
                Age = actor.Age,
                PhotoFileName = actor.Imgurl,
                Movies = actor.Movies.ToList()
            };
        }
    }
}