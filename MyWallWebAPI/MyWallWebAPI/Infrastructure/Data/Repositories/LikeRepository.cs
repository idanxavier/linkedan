using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Infrastructure.Data.Repositories
{
    public class LikeRepository
    {
        private readonly MySQLContext _context;

        public LikeRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Like> NewLike(Like like)
        {
            var ret = await _context.Like.AddAsync(like);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<Like> GetLike(int likeId)
        {
            Like like = await _context.Like.Include(p => p.ApplicationUser).Include(p => p.Post).FirstOrDefaultAsync(p => p.Id == likeId);

            return like;
        }

        public async Task<Like> GetLikeByPostIdAndApplicationUser(int postId, string currentUserId)
        {
            Like like = await _context.Like.FirstOrDefaultAsync(p => p.PostId == postId && p.ApplicationUserId == currentUserId);
            return like;
        }

        public async Task<bool> DeleteLike(int likeId)
        {
            var item = await _context.Like.FindAsync(likeId);
            _context.Like.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Like>> ListUserLikes(string userId)
        {
            List<Like> list = await _context.Like.Where(p => p.ApplicationUserId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.ApplicationUser).Include(p => p.Post).ToListAsync();

            return list;
        }
    }
}
