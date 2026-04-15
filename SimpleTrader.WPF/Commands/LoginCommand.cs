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
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }




        public bool CanExecute(object? parameter)
        {
            return true;
        }

       
        public async void Execute(object? parameter)
        {
            bool result = await  _authenticator.Login(_loginViewModel.Username, parameter?.ToString() ?? string.Empty);
            
            if (result) 
                _renavigator.Renavigate();
            
            
        }

        
    }
}