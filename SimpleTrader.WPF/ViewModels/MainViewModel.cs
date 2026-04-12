using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IRootSimpleTraderViewModelFactory rootSimpleTraderViewModelFactory, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(Navigator, rootSimpleTraderViewModelFactory);
            
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
