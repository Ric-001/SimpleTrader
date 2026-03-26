using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Results;
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
            using FinancialModelingPrepHttpCliente client = new (_options);
            
            var endpoint = _options.ProfileEndpoint.TrimStart('/');

            //"https://financialmodelingprep.com/stable/profile?symbol=AAPL"

            string url = $"/{endpoint}?symbol={symbol}";

            StockPriceDto stockPrice = await client.GetAsync<StockPriceDto>(url);

            return stockPrice.Price;
        }
    }
}
