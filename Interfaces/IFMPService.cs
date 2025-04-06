using WebApplication1.Entity;

namespace WebApplication1.Interfaces
{
    public interface IFMPService
    {

        Task<Stock> FindStockBySymbolAsync(string symbol);

    }
}
