using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationService;
using SimpleTrader.WPF.State.Accounts;
using static SimpleTrader.Domain.Services.AuthenticationService.IAuthenticationService;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        public event Action? StateChanged;

        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        
        public Account? CurrentAccount 
        { 
            get => _accountStore.CurrentAccount; 
            private set 
            {
                if (value != null)
                {
                    _accountStore.CurrentAccount = value;
                    StateChanged?.Invoke();
                }
            } 
        }

        public bool IsLoggedIn => CurrentAccount != null;
        
        
        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        

        public async Task<bool> Login(string username, string password)
        {
            bool result = true;

            try
            {
                CurrentAccount = await _authenticationService.Login(username, password);
            }
            catch (Exception)
            {
                result = false;
            }
            
            return result;
        }

        public void Logout()
        {
            CurrentAccount = null!;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
