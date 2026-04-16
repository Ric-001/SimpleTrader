using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

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
            try
            {
                await _authenticator.Login(_loginViewModel.Username, parameter?.ToString() ?? string.Empty);
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "El usuario no existe.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Contraseña incorrecta.";
            }
            catch (Exception ex)
            {
                _loginViewModel.ErrorMessage = "No se ha podido registrar al usuario.";
            }
        }

        
    }
}