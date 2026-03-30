using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Factories
{
    public interface ISimpleTraderViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}