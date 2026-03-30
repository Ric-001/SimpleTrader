using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Factories
{
    public class HomeViewModelFactory : ISimpleTraderViewModelFactory<HomeViewModel>
    {
        private ISimpleTraderViewModelFactory<MajorIndexListingViewModel> _majorIndexListingViewModelFactory;

        public HomeViewModelFactory(ISimpleTraderViewModelFactory<MajorIndexListingViewModel> majorIndexListingViewModelFactory)
        {
            _majorIndexListingViewModelFactory = majorIndexListingViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_majorIndexListingViewModelFactory.CreateViewModel());
        }
    }
}
