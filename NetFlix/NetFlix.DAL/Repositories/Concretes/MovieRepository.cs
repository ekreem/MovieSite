using Microsoft.EntityFrameworkCore;
using NetFlix.CORE.Entities;
using NetFlix.DAL.Contexts;
using NetFlix.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.DAL.Repositories.Concretes
{
    public class MovieRepository :   IMovieRepository
    {
        AppDbContext _appDbContext;

        public MovieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateMovie(Movie movie)
        {
            await _appDbContext.Movies.AddAsync(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            _appDbContext.Movies.Remove(movie);
        }



        public void UpdateMovie(Movie movie)
        {
            _appDbContext.Movies.Update(movie);
        }
        public async Task<List<Movie>> GetAllWithActorsAsync()
        {
            return await _appDbContext.Movies
                .Include(m => m.Actors)
                .ToListAsync();
        }
        public async Task<Movie> GetByIdWithActorsAsync(int id)
        {
            return await _appDbContext.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task SaveAllChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public Task<List<Movie>> GetAllMovies()
        {
            throw new NotImplementedException();
        }
    }
}
