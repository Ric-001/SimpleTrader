using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using static SimpleTrader.Domain.Services.AthenticationService.IAuthenticationService;

namespace SimpleTrader.Domain.Services.AthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountService _accountService;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountService.GetByUsername(username);
            
            bool passwordsMatch = _passwordHasher.Verify(password, storedAccount.AccountHolder.PasswordHash);
 
            if (!passwordsMatch) 
                throw new InvalidPasswordException(username, password);
            
            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return RegistrationResult.PasswordsDoNotMatch;

            Account emailAccount = await _accountService.GetByEmail(email);

            if (emailAccount != null)
                return RegistrationResult.EmailAlreadyExists;

            Account usernameAccount = await _accountService.GetByUsername(username);
            
            if (usernameAccount != null)
                return RegistrationResult.UsernameAlreadyExists;

            string hashedPassword = _passwordHasher.Hash(password);

            User user = new User()
            {
                Email = email,
                Username = username,
                PasswordHash = hashedPassword,
                DateJoined = DateTime.Now,
            };

            Account account = new Account()
            {
                AccountHolder = user,
            };

            await _accountService.Create(account);

            return RegistrationResult.Success;
        }
    }
}