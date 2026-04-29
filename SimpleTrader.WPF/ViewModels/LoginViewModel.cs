using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        
        private string _username = "test";
#if DEBUG
        private string _password = "test";
#else
        private string _password = string.Empty;
#endif
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public MessageViewModel ErrorMessageViewModel { get; } = new();
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }


        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            _authenticator = authenticator;
            LoginCommand = new LoginCommand(this, _authenticator, loginRenavigator);
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }

        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
