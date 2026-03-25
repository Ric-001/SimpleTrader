using SimpleTrader.FinancialModelingPrepAPI.Services;
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
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is not ViewType viewType) return;

            _navigator.CurrentViewModel = viewType switch
            {
                ViewType.Home => new HomeViewModel(MajorIndexListingViewModel.LoadMajorIndexViewModel(new MajorIndexService())),
                ViewType.Portfolio => new PortfolioViewModel(),
                _ => _navigator.CurrentViewModel
            };
        }
    }
}
