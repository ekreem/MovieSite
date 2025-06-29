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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IActorRepository _aRepository;
        private readonly IWebHostEnvironment _environment;

        public MovieService(IMovieRepository repository, IActorRepository aRepository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _aRepository = aRepository;
            _environment = environment;
        }

        public async Task CreateMovie(CreateMovieVm movieVm)
        {
            var actors = await _aRepository.GetAllAsync();
            var selectedActors = actors.Where(actor => movieVm.ActorsIds.Contains(actor.Id)).ToList();

           

            string fileName = null;
            if (movieVm.File != null && movieVm.File.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(movieVm.File.FileName);
                var path = Path.Combine(_environment.WebRootPath, "Upload", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await movieVm.File.CopyToAsync(fileStream);
                }
            }

            var movie = new Movie
            {
                Title = movieVm.Title,
                Category = movieVm.Category,
                Description = movieVm.Description,
                Director = movieVm.Director,
                Year = movieVm.Year,
                DurationMinutes = movieVm.DurationMinutes,
                ImdbRating = movieVm.ImdbRating,
                PosterFileName = fileName != null ? "/Upload/" + fileName : string.Empty,
                TrailerUrl = movieVm.TrailerUrl,
                Quality = movieVm.Quality,
                Actors = selectedActors
            };

            await _repository.CreateMovie(movie);
            await _repository.SaveAllChangesAsync();
        }

        public async void UpdateMovie(UpdateMovieVm movieVm)
        {
            var movie = await _repository.GetByIdWithActorsAsync(movieVm.Id);
            if (movie == null)
                throw new ArgumentException($"Movie with ID {movieVm.Id} not found.");

            var actors = await _aRepository.GetAllAsync();
            var selectedActors = actors.Where(actor => movieVm.ActorsIds.Contains(actor.Id)).ToList();

            if (selectedActors == null || !selectedActors.Any())
                throw new ArgumentException("No valid actors selected.");

            string fileName = movie.PosterFileName;
            if (movieVm.File != null && movieVm.File.Length > 0)
            {
                if (!string.IsNullOrEmpty(movie.PosterFileName))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, movie.PosterFileName.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(movieVm.File.FileName);
                var path = Path.Combine(_environment.WebRootPath, "Upload", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await movieVm.File.CopyToAsync(fileStream);
                }
            }

            movie.Title = movieVm.Title;
            movie.Category = movieVm.Category;
            movie.Description = movieVm.Description;
            movie.Director = movieVm.Director;
            movie.Year = movieVm.Year;
            movie.DurationMinutes = movieVm.DurationMinutes;
            movie.ImdbRating = movieVm.ImdbRating;
            movie.PosterFileName = fileName != null ? "/Upload/" + fileName : movie.PosterFileName;
            movie.TrailerUrl = movieVm.TrailerUrl;
            movie.Quality = movieVm.Quality;
            movie.Actors = selectedActors;

            _repository.UpdateMovie(movie);
            await _repository.SaveAllChangesAsync();
        }

        public async void DeleteMovie(int id)
        {
            var movie = await _repository.GetByIdWithActorsAsync(id);
            if (movie == null)
                throw new ArgumentException($"Movie with ID {id} not found.");

            if (!string.IsNullOrEmpty(movie.PosterFileName))
            {
                var filePath = Path.Combine(_environment.WebRootPath, movie.PosterFileName.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            _repository.DeleteMovie(movie);
            await _repository.SaveAllChangesAsync();
        }

        public async Task<List<GetMovieVm>> GetAllMoviesAsync()
        {
            var movies = await _repository.GetAllWithActorsAsync();
            return movies.Select(m => MapToGetMovieVm(m)).ToList();
        }

        public async Task<GetMovieVm> GetMovieByIdAsync(int id)
        {
            var movie = await _repository.GetByIdWithActorsAsync(id);
            if (movie == null)
                return null;
            return MapToGetMovieVm(movie);
        }

        public async Task SaveAllChangesAsync()
        {
            await _repository.SaveAllChangesAsync();
        }

        private GetMovieVm MapToGetMovieVm(Movie movie)
        {
            return new GetMovieVm
            {
                Title = movie.Title,
                Category = movie.Category,
                Description = movie.Description,
                Director = movie.Director,
                Actors = movie.Actors?.ToList() ?? new List<Actor>(),
                Year = movie.Year,
                DurationMinutes = movie.DurationMinutes,
                ImdbRating = movie.ImdbRating,
                PosterFileName = movie.PosterFileName,
                TrailerUrl = movie.TrailerUrl,
                Quality = movie.Quality
            };
        }
    }
}