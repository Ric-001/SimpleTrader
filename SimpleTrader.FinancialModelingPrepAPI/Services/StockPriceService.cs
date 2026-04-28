using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Results;


namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpCliente _client;

        public StockPriceService(FinancialModelingPrepHttpCliente client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        

        public async Task<double> GetPrice(string symbol)
        {
            
            StockPriceDto stockPrice = await _client.GetAsync<StockPriceDto>(symbol);

            if (stockPrice.Price == 0)
                throw new InvalidSymbolException(symbol);

            return stockPrice.Price;
        }
    }
}
