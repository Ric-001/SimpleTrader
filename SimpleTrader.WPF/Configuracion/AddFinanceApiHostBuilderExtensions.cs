using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.FinancialModelingPrepAPI.Options;


namespace SimpleTrader.WPF.Configuracion
{
    public static class AddFinanceApiHostBuilderExtensions
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
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

                services.AddHttpClient<FinancialModelingPrepHttpCliente>(client =>
                {
                    client.BaseAddress = new Uri(fmpOptions.BaseUrl);
                });
            });
            return host;
        }
    }
}