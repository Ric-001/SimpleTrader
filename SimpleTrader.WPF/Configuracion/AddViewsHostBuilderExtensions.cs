using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Configuracion
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(sp =>
                    new MainWindow(sp.GetRequiredService<MainViewModel>()));
            });
            return host;
        }
    }
}