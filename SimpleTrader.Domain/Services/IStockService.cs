namespace SimpleTrader.Domain.Services
{
    public interface IStockService
    {
        Task<double> GetPrice(string symbol);
    }
}
