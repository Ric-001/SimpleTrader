using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        
        private string _username = string.Empty;
        //private string _password = string.Empty;
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        //public string Password { get => _password; set => SetProperty(ref _password, value); }
        public ICommand LoginCommand { get; }



        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            LoginCommand = new LoginCommand(this, authenticator);
        }
    }
}
