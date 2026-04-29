using SimpleTrader.WPF.ViewModels;


namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
        public event Action? StateChanged;
        
        private ViewModelBase? _currentViewModel;


        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel; set { _currentViewModel?.Dispose(); _currentViewModel = value; StateChanged?.Invoke(); }
        }
    }
}
