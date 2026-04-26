using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;

namespace SimpleTrader.WPF.Configuracion
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<SimpleTraderDbContextFactory>();
            });
            return host;
        }
    }
}