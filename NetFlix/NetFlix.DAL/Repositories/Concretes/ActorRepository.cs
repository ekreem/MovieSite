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
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Actor>> GetAllAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<List<Actor>> GetAllWithMoviesAsync()
        {
            return await _context.Actors
                .Include(a => a.Movies)
                .ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }
        public async Task<Actor> GetByIdWithMoviesAsync(int id)
        {
            return await _context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateActorAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            _context.Actors.Update(actor);
        }
        public async Task DeleteActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }
        }
        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
