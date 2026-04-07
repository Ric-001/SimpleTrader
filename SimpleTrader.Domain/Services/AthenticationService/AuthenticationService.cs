using SimpleTrader.Domain.Models;
using BCrypt.Net;

namespace SimpleTrader.Domain.Services.AthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataService<Account> _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IDataService<Account> accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public Task<Account> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return false;

            string hashedPassword = _passwordHasher.Hash(password);

            User user = new User()
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
            };

            Account account = new Account()
            {
                AccountHolder = user,
            };

            await _accountService.Create(account);

            return true;
        }
    }

    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }

    public class BcryptPasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 12;

        public string Hash(string password) =>BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);

        public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
