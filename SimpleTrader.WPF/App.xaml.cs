using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
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
        public static IMajorIndexService MajorIndexService { get; private set; } = null!;

        protected override async void OnStartup(StartupEventArgs e)
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

            // 3.Crear servicios con DI usando las opciones
            IServiceProvider serviceProvider = CreateServiceProvider(configuration, fmpOptions);
            
            // 2. Crear VM principal inyectando el servicio
            var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();

            //// 3. Crear ventana principal
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider(IConfiguration configuration, FinancialModelingPrepOptions fmpOptions)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<FinancialModelingPrepOptions>(fmpOptions);

            services.AddSingleton<SimpleTraderDbContextFactory>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();

            services.AddSingleton<IRootSimpleTraderViewModelFactory, RootSimpleTraderViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<PortfolioViewModel>, PortfolioViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<MajorIndexListingViewModel>, MajorIndexListingViewModelFactory>();


            services.AddScoped<BuyViewModel>();
            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }

}
