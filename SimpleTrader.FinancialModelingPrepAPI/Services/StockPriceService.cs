using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using System.Text.Json;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockService
    {
        private readonly FinancialModelingPrepOptions _options;

        public StockPriceService(FinancialModelingPrepOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
                throw new InvalidOperationException("ApiKey de FinancialModelingPrep no configurada.");
        }

        public async Task<double> GetPrice(string symbol)
        {
            using HttpClient client = new HttpClient();
            var baseUrl = _options.BaseUrl.TrimEnd('/');
            var endpoint = _options.ProfileEndpoint.TrimStart('/');

            string url = $"{baseUrl}/{endpoint}?symbol={symbol}&apikey={_options.ApiKey}"; 

            HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            //List<MajorIndexDto>? dtoList = JsonSerializer.Deserialize<List<MajorIndexDto>>(jsonResponse, options);

            //if (dtoList == null || dtoList.Count == 0)
            //    throw new Exception("No se han recibido datos del índice.");

            //MajorIndexDto dto = dtoList[0];

            //MajorIndex majorIndex = new MajorIndex
            //{
            //    Price = dto.Price,
            //    Changes = dto.Change,
            //    Type = indexType
            //};

            return 12.0;
        }
    }
}
