using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly ISimpleTraderViewModelFactory<TViewModel> _viewModelFactory;
        private readonly INavigator _navigator;
        public ViewModelFactoryRenavigator(INavigator navigator, ISimpleTraderViewModelFactory<TViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
        }
        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
