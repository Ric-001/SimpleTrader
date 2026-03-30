using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Factories
{
    public class SimpleTraderViewModelAbastractFactory : ISimpleTraderViewModelAbastractFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;

        public SimpleTraderViewModelAbastractFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _homeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _portfolioViewModelFactory.CreateViewModel(),
                _ => throw new ArgumentException("El ViewType no tiene ningún ViewModel")
            };
        }
    }
}
