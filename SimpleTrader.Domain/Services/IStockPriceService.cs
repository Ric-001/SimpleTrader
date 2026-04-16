using SimpleTrader.Domain.Exceptions;
namespace SimpleTrader.Domain.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Obtiene el precio actual de una acción dado su símbolo. Este método es asíncrono y puede lanzar excepciones si el símbolo no es válido o si no se obtiene el precio de la acción.
        /// </summary>
        /// <param name="symbol">El símbolo de la acción.</param>
        /// <returns>El precio actual de la acción.</returns>
        /// <exception cref="InvalidSymbolException">Se lanza si el símbolo no es válido.</exception>
        /// <exception cref="Exception">Se lanza si falla al obtener el precio de la acción.</exception>
        Task<double> GetPrice(string symbol);
    }
}
