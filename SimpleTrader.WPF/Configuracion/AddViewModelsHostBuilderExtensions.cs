using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.Configuracion
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                // Factory de AssetListingViewModel parametrizado
                services.AddSingleton<Func<int?, AssetListingViewModel>>(sp =>
                {
                    AssetStore assetStore = sp.GetRequiredService<AssetStore>();
                    return (int? maxNumber) => new AssetListingViewModel(assetStore, maxNumber);
                });

                // ViewModels de página
                services.AddTransient<AssetSummaryViewModel>();
                services.AddTransient<BuyViewModel>();
                services.AddTransient<SellViewModel>();
                services.AddTransient<PortfolioViewModel>();
                services.AddTransient(CreateHomeViewModel);

                // CreateViewModel delegates
                services.AddSingleton<CreateViewModel<HomeViewModel>>(sp => () => CreateHomeViewModel(sp));
                services.AddSingleton<CreateViewModel<BuyViewModel>>(sp => () => sp.GetRequiredService<BuyViewModel>());
                services.AddSingleton<CreateViewModel<SellViewModel>>(sp => () => sp.GetRequiredService<SellViewModel>());
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>(sp => () => sp.GetRequiredService<PortfolioViewModel>());
                services.AddSingleton<CreateViewModel<LoginViewModel>>(sp => () => CreateLoginViewModel(sp));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(sp => () => CreateRegisterViewModel(sp));

                // Factory principal y renavigators
                services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();

                services.AddSingleton<MainViewModel>();
            });
            return host;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider sp)
        {
            return new HomeViewModel(
                sp.GetRequiredService<AssetSummaryViewModel>(),
                MajorIndexListingViewModel.LoadMajorIndexViewModel(
                    sp.GetRequiredService<IMajorIndexService>()));
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider sp)
        {
            return new LoginViewModel(
                sp.GetRequiredService<IAuthenticator>(),
                sp.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                sp.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider sp)
        {
            return new RegisterViewModel(
                sp.GetRequiredService<IAuthenticator>(),
                sp.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                sp.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
        }
    }
}