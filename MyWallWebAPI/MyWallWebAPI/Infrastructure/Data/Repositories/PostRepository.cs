using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Infrastructure.Data.Repositories
{
    public class PostRepository
    {
        private readonly MySQLContext _context;

        public PostRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> ListPosts(string userId)
        {
            List<Post> list = await _context.Post.Where(p => !p.ApplicationUserId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.ApplicationUser).ToListAsync();
            return list;
        }
            
        public async Task<List<Post>> ListUserPosts(string userId)
        {
            List<Post> list = await _context.Post.Where(p => p.ApplicationUserId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.ApplicationUser).ToListAsync();

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _context.Post.Include(p => p.ApplicationUser).Include(p => p.LikePost).FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            var ret = await _context.Post.AddAsync(post);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePost(int postId)
        {
            var item = await _context.Post.FindAsync(postId);
            _context.Post.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}