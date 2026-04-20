using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.WPF.Configuracion;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace SimpleTrader.WPF
{
    public partial class App : Application
    {
        private IHost _host;
        //private IServiceProvider _serviceProvider = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureCulture();

            _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var fmpOptions = BuildFmpOptions(context.Configuration);
                services.AddInfrastructure(context.Configuration, fmpOptions)
                        .AddPresentation();
            })
            .Build();

            _host.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        // ── Cultura ────────────────────────────────────────────────────────────────

        private static void ConfigureCulture()
        {
            var culture = new CultureInfo("es-ES");
            
            // Hilo principal
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Todos los hilos nuevos (ThreadPool, async/await, etc.)
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }

        
        private static FinancialModelingPrepOptions BuildFmpOptions(IConfiguration configuration)
        {
            var options = configuration
                .GetSection("FinancialModelingPrep")
                .Get<FinancialModelingPrepOptions>() ?? new();

            options.ApiKey = Environment.GetEnvironmentVariable("FMP_API_KEY")
                ?? AbortWithError("La variable de entorno 'FMP_API_KEY' no está configurada.");

            return options;
        }

        // ── Helpers ───────────────────────────────────────────────────────────────

        [DoesNotReturn]
        private static string AbortWithError(string message)
        {
            MessageBox.Show(message, "Error de configuración",
                MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
            throw new InvalidOperationException(message); // satisface al compilador
        }
    }
}