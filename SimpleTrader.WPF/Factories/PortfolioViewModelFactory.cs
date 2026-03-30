using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Factories
{
    public class PortfolioViewModelFactory : ISimpleTraderViewModelFactory<PortfolioViewModel>
    {
        public PortfolioViewModel CreateViewModel()
        {
            return new PortfolioViewModel();
        }
    }
}