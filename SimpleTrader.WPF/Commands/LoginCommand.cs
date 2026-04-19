using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;


namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        protected override async Task ExecuteAsync(object? parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;
            await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);
            _renavigator.Renavigate();
        }

        protected override void OnExecutionFailed(Exception ex)
        {
            _loginViewModel.ErrorMessage = ex switch
            {
                UserNotFoundException => "El usuario no existe.",
                InvalidPasswordException => "Contraseña incorrecta.",
                _ => "No se ha podido iniciar sesión."
            };
        }
    }
}