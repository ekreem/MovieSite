using NetFlix.CORE.Entities;
using NetFlix.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Abstracts
{
    public interface IMovieService
    {
        Task CreateMovie(CreateMovieVm movie);
        void UpdateMovie(UpdateMovieVm movie);

        Task<List<GetMovieVm>> GetAllMoviesAsync();
        Task<GetMovieVm> GetMovieByIdAsync(int id);

        void DeleteMovie(int id);

        Task SaveAllChangesAsync();
    }
}
