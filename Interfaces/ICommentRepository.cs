using WebApplication1.Entity;

namespace WebApplication1.Interfaces
{
    public interface ICommentRepository<T> where T : Comment
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T commentModel);

        Task<T?> UpdateAsync(int id, T commentModel);

        Task<T?> DeleteAsync(int id);
    }
}
