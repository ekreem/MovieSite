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
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;
        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Movie)
                    .ThenInclude(m => m.Actors)
                .Where(f => f.AppUserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(string userId, int movieId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.AppUserId == userId && f.MovieId == movieId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
