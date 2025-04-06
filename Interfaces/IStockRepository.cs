using WebApplication1.DTOs.Stock;
using WebApplication1.Entity;
using WebApplication1.Helpers;

namespace WebApplication1.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> GetBySymbolAsync(string symbol);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExist(int id);

    }
}
