using NetFlix.BLL.Services.Abstracts;
using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using NetFlix.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Concretes
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<List<GetMovieVm>> GetFavoritesByUserIdAsync(string userId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserIdAsync(userId);
            return favorites.Select(f => new GetMovieVm
            {
                Id = f.Movie.Id,
                Title = f.Movie.Title,
                Category = f.Movie.Category,
                Description = f.Movie.Description,
                Director = f.Movie.Director,
                Actors = f.Movie.Actors.ToList(),
                Year = f.Movie.Year,
                DurationMinutes = f.Movie.DurationMinutes,
                ImdbRating = f.Movie.ImdbRating,
                PosterFileName = f.Movie.PosterFileName,
                TrailerUrl = f.Movie.TrailerUrl,
                TmdbId = (int)f.Movie.TmdbId,
                Quality = f.Movie.Quality
            }).ToList();
        }

        public async Task AddFavoriteAsync(string userId, int movieId)
        {
            var favorite = new Favorite
            {
                AppUserId = userId,
                MovieId = movieId
            };

            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavoriteAsync(string userId, int movieId)
        {
            await _favoriteRepository.RemoveAsync(userId, movieId);
        }
        public async Task<bool> IsFavoriteAsync(string userId, int movieId)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserIdAsync(userId);
            return favorites.Any(f => f.MovieId == movieId);
        }
    }
}
