using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.Factories;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly INavigator _navigator;
        private readonly ISimpleTraderViewModelAbastractFactory _viewModelFactory;
        
        public UpdateCurrentViewModelCommand(INavigator navigator, ISimpleTraderViewModelAbastractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is not ViewType viewType) return;

            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

        }
    }
}
