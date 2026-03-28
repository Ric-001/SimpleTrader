using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Results;


namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
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
            
            //"https://financialmodelingprep.com/stable/profile?symbol=AAPL"

            StockPriceDto stockPrice = await client.GetAsync<StockPriceDto>(symbol);

            if (stockPrice.Price == 0)
                throw new InvalidSymbolException(symbol);

            return stockPrice.Price;
        }
    }
}
