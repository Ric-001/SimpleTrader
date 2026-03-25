using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockService
    {
        private const string API_KEY = "RYiPoM5uCF5boZ9HUh5u8pZ3w0k9yFcF";
        public async Task<double> GetPrice(string symbol)
        {
            using HttpClient client = new HttpClient();
            string url = $"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={API_KEY}";

            HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<MajorIndexDto>? dtoList = JsonSerializer.Deserialize<List<MajorIndexDto>>(jsonResponse, options);

            if (dtoList == null || dtoList.Count == 0)
                throw new Exception("No se han recibido datos del índice.");

            MajorIndexDto dto = dtoList[0];

            MajorIndex majorIndex = new MajorIndex
            {
                Price = dto.Price,
                Changes = dto.Change,
                Type = indexType
            };

            return majorIndex;
        }
    }
}
