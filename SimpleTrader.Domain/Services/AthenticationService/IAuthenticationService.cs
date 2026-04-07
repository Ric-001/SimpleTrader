using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services.AthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string username, string password, string conffirmPassword);
        Task<Account> Login(string username, string password); 
    }
}
