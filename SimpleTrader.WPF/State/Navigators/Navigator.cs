using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
        public event Action StateChanged;
        
        private ViewModelBase? _currentViewModel;


        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel; set { _currentViewModel = value; StateChanged?.Invoke(); }
        }
    }
}
