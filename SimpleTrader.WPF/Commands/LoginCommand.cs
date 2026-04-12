using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            
        }




        public bool CanExecute(object? parameter)
        {
            return true;
        }

       
        public async void Execute(object? parameter)
        {
            bool result = await  _authenticator.Login(_loginViewModel.Username, parameter?.ToString() ?? string.Empty);
            
            
            
        }

        
    }
}