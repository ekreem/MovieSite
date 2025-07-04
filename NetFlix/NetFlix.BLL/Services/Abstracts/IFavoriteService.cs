using NetFlix.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.BLL.Services.Abstracts
{
    public interface IFavoriteService
    {
        Task<List<GetMovieVm>> GetFavoritesByUserIdAsync(string userId);
        Task AddFavoriteAsync(string userId, int movieId);
        Task RemoveFavoriteAsync(string userId, int movieId);
        Task<bool> IsFavoriteAsync(string userId, int movieId);
    }
}
