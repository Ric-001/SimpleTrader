using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Compra acciones de una acción específica para una cuenta establecida. Este método es asíncrono y puede lanzar excepciones si el comprador no tiene suficiente saldo para completar la compra o si ocurre algún error durante la transacción.
        /// </summary>
        /// <param name="buyer">La cuenta del comprador.</param>
        /// <param name="stock">El símbolo de la acción a comprar.</param>
        /// <param name="shares">El número de acciones a comprar.</param>
        /// <returns>La cuenta del comprador actualizada después de la compra.</returns>
        /// <exception cref="InsufficientFundsException">Se lanza si el comprador no tiene suficiente saldo para completar la compra.</exception>
        /// <exception cref="InvalidSymbolException">Se lanza si el símbolo de la acción no es válido.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la transacción.</exception>
        Task<Account> BuyStock(Account buyer, string stock, int shares);
    }
}
