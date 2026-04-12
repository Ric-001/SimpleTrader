using SimpleTrader.Domain.Models;
using static SimpleTrader.Domain.Services.AuthenticationService.IAuthenticationService;

namespace SimpleTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        event Action StateChanged;
        Account? CurrentAccount { get; }
        bool IsLoggedIn { get; }
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        Task<bool> Login(string email, string password);
        void Logout();
    }
}
