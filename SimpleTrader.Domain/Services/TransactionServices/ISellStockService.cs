using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Vende acciones de un símbolo específico desde la cuenta del vendedor.
        /// </summary>
        /// <param name="seller">La cuenta del vendedor.</param>
        /// <param name="symbol">El símbolo de la acción a vender.</param>
        /// <param name="shares">La cantidad de acciones a vender.</param>
        /// <returns>La cuenta actualizada del vendedor después de la venta.</returns>
        /// <exception cref="InsufficientSharesException">Se lanza si no hay suficientes acciones para vender.</exception>
        /// <exception cref="InvalidSymbolException">Se lanza si el símbolo de la acción no es válido.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la transacción.</exception>
        Task<Account> SellStock(Account seller, string symbol, int shares);
    }
}
