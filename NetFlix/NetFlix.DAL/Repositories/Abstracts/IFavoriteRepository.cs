using NetFlix.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.DAL.Repositories.Abstracts
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetFavoritesByUserIdAsync(string userId);
        Task AddAsync(Favorite favorite);
        Task RemoveAsync(string userId, int movieId);
    }
}
