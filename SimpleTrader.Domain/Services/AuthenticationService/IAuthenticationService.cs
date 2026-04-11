using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        public enum RegistrationResult
        {
            Success,
            PasswordsDoNotMatch,
            EmailAlreadyExists,
            UsernameAlreadyExists
        }

        Task<RegistrationResult> Register(string email, string username, string password, string conffirmPassword);
        Task<Account> Login(string username, string password); 
    }
}
