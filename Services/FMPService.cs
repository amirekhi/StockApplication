using WebApplication1.DTOs.Stock;
using WebApplication1.Entity;
using WebApplication1.Interfaces;
using Newtonsoft.Json;
using WebApplication1.Mappers;

namespace WebApplication1.Services
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
                if (result.IsSuccessStatusCode)
                {

                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<FMPStocks[]>(content);

                    var stock = tasks[0];
                    if (stock != null)
                    {

                        return stock.ToStockFromFMP();
                    }

                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


    }
}
