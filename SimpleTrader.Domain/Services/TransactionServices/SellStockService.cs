using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;
        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            //Validar si el vendedor tiene suficientes acciones para vender
            int accountShares = GetAccountSharesForSymbol(seller, symbol);

            if(accountShares < shares) 
                throw new InsufficientSharesException("No hay suficientes acciones para vender.", symbol, accountShares, shares);
            

            // Obtener el precio de la acción

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            // Crear una transacción de venta
            double transactionPrice = stockPrice * shares;

            AssetTransaction transaction = new AssetTransaction
            {
                Account = seller,
                Asset = new Asset { Symbol = symbol, PricePerShare = stockPrice },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = false
            };
            
            seller.AssetTransactions.Add(transaction);
            seller.Balance += transactionPrice;

            //Actualizar la cuenta del vendedor en la base de datos

            await _accountService.Update(seller.Id, seller);
            
            
            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            var accountTransactionsForSymbol = seller.AssetTransactions.Where(t => t.Asset.Symbol == symbol);
            return accountTransactionsForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}