using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : IRootSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;

        private readonly BuyViewModel _buyViewModel;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<LoginViewModel> loginViewModelFactory, ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory,
            ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory, BuyViewModel buyViewModel)
        {
            _loginViewModelFactory = loginViewModelFactory;
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Login => _loginViewModelFactory.CreateViewModel(),
                ViewType.Home => _homeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _portfolioViewModelFactory.CreateViewModel(),
                ViewType.Buy => _buyViewModel,
                _ => throw new ArgumentException("El ViewType no tiene ningún ViewModel")
            };
        }
    }
}
