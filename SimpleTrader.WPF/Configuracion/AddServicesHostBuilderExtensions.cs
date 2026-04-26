using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationService;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Services;

namespace SimpleTrader.WPF.Configuracion
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                // FMP Options — se construye aquí usando IConfiguration + variable de entorno
                var fmpOptions = context.Configuration
                    .GetSection("FinancialModelingPrep")
                    .Get<FinancialModelingPrepOptions>() ?? new();

                fmpOptions.ApiKey = context.Configuration["FMP_API_KEY"]
                    ?? throw new InvalidOperationException(
                        "La variable de entorno 'FMP_API_KEY' no está configurada.");

                services.AddSingleton(fmpOptions);

                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IMajorIndexService, MajorIndexService>();
                services.AddSingleton<IStockPriceService, StockPriceService>();
                services.AddSingleton<IBuyStockService, BuyStockService>();
                services.AddSingleton<ISellStockService, SellStockService>();
                services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
            });
            return host;
        }
    }
}