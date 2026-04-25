using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelFactory : ISimpleTraderViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
        private readonly CreateViewModel<SellViewModel> _createSellViewModel;

        public SimpleTraderViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<LoginViewModel> createLoginViewModel, 
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, CreateViewModel<BuyViewModel> createBuyViewModel, CreateViewModel<SellViewModel> createSellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createBuyViewModel = createBuyViewModel;
            _createSellViewModel = createSellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Login => _createLoginViewModel(),
                ViewType.Home => _createHomeViewModel(),
                ViewType.Portfolio => _createPortfolioViewModel(),
                ViewType.Buy => _createBuyViewModel(),
                ViewType.Sell => _createSellViewModel(),
                _ => throw new ArgumentException("El ViewType no tiene ningún ViewModel")
            };
        }
    }
}
