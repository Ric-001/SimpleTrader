using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Services.AuthenticationService;
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
        private IServiceProvider _serviceProvider = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureCulture();

            var configuration = BuildConfiguration();
            var fmpOptions = BuildFmpOptions(configuration);

            _serviceProvider = new ServiceCollection()
                .AddInfrastructure(configuration, fmpOptions)
                .AddPresentation()
                .BuildServiceProvider();

            IAuthenticationService authService = _serviceProvider.GetRequiredService<IAuthenticationService>();
            authService.Login("testuser", "password123");
            _serviceProvider.GetRequiredService<MainWindow>().Show();
        }

        // ── Cultura ────────────────────────────────────────────────────────────────

        private static void ConfigureCulture()
        {
            var culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }

        // ── Configuración ──────────────────────────────────────────────────────────

        private static IConfiguration BuildConfiguration() => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

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