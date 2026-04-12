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
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, ISimpleTraderViewModelFactory SimpleTraderViewModelFactory, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _navigator.StateChanged += OnNavigatorStateChanged;
            _authenticator = authenticator;
            _authenticator.StateChanged += OnAuthenticatorStateChanged;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, SimpleTraderViewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void OnNavigatorStateChanged() => OnPropertyChanged(nameof(CurrentViewModel));

        private void OnAuthenticatorStateChanged() => OnPropertyChanged(nameof(IsLoggedIn));
        
    }
}
