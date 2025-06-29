using NetFlix.CORE.Entities;
using NetFlix.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.DAL.Repositories.Abstracts
{
    public interface IMovieRepository
    {
        Task CreateMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Task<List<Movie>> GetAllWithActorsAsync();
        Task<Movie> GetByIdWithActorsAsync(int id);
        void DeleteMovie(Movie movie);
        Task<List<Movie>> GetAllMovies();
        Task SaveAllChangesAsync();
    }   
}
