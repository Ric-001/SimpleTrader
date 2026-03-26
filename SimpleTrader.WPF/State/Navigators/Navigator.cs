using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.ViewModels;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
    public class Navigator : ObservableModel, INavigator
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }
        
        public ICommand UpdateCurrentViewModelCommand { get; } 


        public Navigator(IMajorIndexService majorIndexService)
        {
            // Inyectamos el servicio y creamos el comando UNA VEZ
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, majorIndexService);
        }



    }
}
