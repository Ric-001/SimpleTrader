using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationService;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Options;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.Configuracion
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, FinancialModelingPrepOptions fmpOptions)
        {
            services.AddSingleton(configuration);
            services.AddSingleton(fmpOptions);

            services.AddSingleton<SimpleTraderDbContextFactory>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<ISellStockService, SellStockService>();
            
            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

            return services;
        }

        internal static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Estado compartido
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<AssetStore>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();

            // Factory de AssetListingViewModel parametrizado por límite de activos
            services.AddSingleton<Func<int?, AssetListingViewModel>>(sp =>
                maxNumber => new AssetListingViewModel(
                    sp.GetRequiredService<AssetStore>(), maxNumber));

            // ViewModels de página
            services.AddSingleton<AssetSummaryViewModel>();
            services.AddSingleton<BuyViewModel>();
            services.AddSingleton<SellViewModel>();
            services.AddSingleton<PortfolioViewModel>();

            // MajorIndexListingViewModel requiere carga asíncrona inicial
            services.AddSingleton<MajorIndexListingViewModel>(sp =>
                MajorIndexListingViewModel.LoadMajorIndexViewModel(
                    sp.GetRequiredService<IMajorIndexService>()));

            services.AddSingleton<HomeViewModel>();

            // Renavigators
            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();

            // Factories de ViewModel (usadas por SimpleTraderViewModelFactory)
            services.AddSingleton<CreateViewModel<HomeViewModel>>(sp =>
                sp.GetRequiredService<HomeViewModel>);

            services.AddSingleton<CreateViewModel<BuyViewModel>>(sp =>
                sp.GetRequiredService<BuyViewModel>);

            services.AddSingleton<CreateViewModel<SellViewModel>>(sp =>
                sp.GetRequiredService<SellViewModel>);

            services.AddSingleton<CreateViewModel<PortfolioViewModel>>(sp =>
                sp.GetRequiredService<PortfolioViewModel>);

            services.AddSingleton<CreateViewModel<LoginViewModel>>(sp => () =>
                new LoginViewModel(
                    sp.GetRequiredService<IAuthenticator>(),
                    sp.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                    sp.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>()));

            services.AddSingleton<CreateViewModel<RegisterViewModel>>(sp => () =>
                new RegisterViewModel(
                    sp.GetRequiredService<IAuthenticator>(),
                    sp.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                    sp.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>()));

            services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();

            // Ventana principal
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(sp =>
                new MainWindow(sp.GetRequiredService<MainViewModel>()));

            return services;
        }
    }
}