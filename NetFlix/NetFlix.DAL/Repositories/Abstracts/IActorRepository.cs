using NetFlix.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.DAL.Repositories.Abstracts
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllAsync();
        Task<List<Actor>> GetAllWithMoviesAsync();
        Task<Actor> GetByIdAsync(int id);
        Task<Actor> GetByIdWithMoviesAsync(int id);
        Task CreateActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(int id);
        Task SaveAllChangesAsync();
    }
}
