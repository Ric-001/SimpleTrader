using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace SimpleTrader.WPF.Configuracion
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(Directory.GetCurrentDirectory());
                c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                c.AddEnvironmentVariables();
            });
            return host;
        }
    }
}