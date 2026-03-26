using Microsoft.Extensions.Configuration;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IStockService StockService { get; private set; } = null!;
        public static IMajorIndexService MajorIndexService { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Cultura
            var culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(culture.IetfLanguageTag)));

            // 1. Construir configuración leyendo appsettings.json del proyecto WPF
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            // 2. Mapear sección FinancialModelingPrep a las opciones
            var fmpOptions = new FinancialModelingPrepOptions();
            configuration.GetSection("FinancialModelingPrep").Bind(fmpOptions);

            // ApiKey desde variable de entorno
            var apiKeyFromEnv = Environment.GetEnvironmentVariable("FMP_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKeyFromEnv))
            {
                MessageBox.Show("La variable de entorno 'FMP_API_KEY' no está configurada.", "Error de configuración",
                    MessageBoxButton.OK,MessageBoxImage.Error);

                Shutdown();
                return;
            }
            fmpOptions.ApiKey = apiKeyFromEnv;

            // 3.Crear servicios con las opciones
            StockService = new StockPriceService(fmpOptions);
            MajorIndexService = new MajorIndexService(fmpOptions);

            // 4. Crear Navigator inyectando el servicio necesario
            var navigator = new Navigator(MajorIndexService);

            // 2. Crear VM principal inyectando el servicio
            var mainViewModel = new MainViewModel(App.MajorIndexService, StockService, navigator);

            // 3. Crear ventana principal
            MainWindow mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            mainWindow.Show();
            
            base.OnStartup(e);
        }
    }

}
