using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using SimpleTrader.WPF.Configuracion;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace SimpleTrader.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            ConfigureCulture();
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            SimpleTraderDbContextFactory contextFactory =
                _host.Services.GetRequiredService<SimpleTraderDbContextFactory>();
            using (SimpleTraderDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            _host.Services.GetRequiredService<MainWindow>().Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private static void ConfigureCulture()
        {
            var culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }
    }
}