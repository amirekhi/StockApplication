using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entity;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories
{
    public class CommentRepository<T> : ICommentRepository<T> where T : Comment
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // Dynamically set the DbSet for T
        }

        public async Task<T> CreateAsync(T commentModel)
        {
            await _dbSet.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<T?> DeleteAsync(int id)
        {
            var commentModel = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                return null;
            }

            _dbSet.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.AppUser).ToListAsync();
         
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            
        }

        public async Task<T?> UpdateAsync(int id, T commentModel)
        {
            var existingComment = await _dbSet.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
