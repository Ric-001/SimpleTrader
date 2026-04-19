using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using static SimpleTrader.Domain.Services.AuthenticationService.IAuthenticationService;


namespace SimpleTrader.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }
        protected override async Task ExecuteAsync(object? parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;
            RegistrationResult result = await _authenticator.Register(_registerViewModel.Email, _registerViewModel.Username, _registerViewModel.Password, _registerViewModel.ConfirmPassword);
            
            switch(result)
            {
                case RegistrationResult.Success:
                    _renavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    _registerViewModel.ErrorMessage = "Las contraseñas no coinciden.";
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    _registerViewModel.ErrorMessage = "El correo electrónico ya existe.";
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    _registerViewModel.ErrorMessage = "El nombre de usuario ya existe.";
                    break;
                default:
                    _registerViewModel.ErrorMessage = "No se ha podido registrar el usuario.";
                    break;
            }

            
        }
        protected override void OnExecutionFailed(Exception ex)
        {
            _registerViewModel?.ErrorMessage = ex.Message;
        }
    }
}