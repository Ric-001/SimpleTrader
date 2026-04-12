using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        
        private string _username = string.Empty;
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        
        public ICommand LoginCommand { get; }



        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            LoginCommand = new LoginCommand(this, authenticator, renavigator);
        }
    }
}
