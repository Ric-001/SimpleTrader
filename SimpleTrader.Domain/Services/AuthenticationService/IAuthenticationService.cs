using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

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
        
        /// <summary>
        /// Authenticates a user with the specified username and password and returns the associated account if the credentials are valid.
        /// </summary>
        /// <param name="username">The username of the account to authenticate. Cannot be null or empty.</param>
        /// <param name="password">The password associated with the specified username. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authenticated account if the
        /// credentials are valid; otherwise, null.</returns>
        /// <exception cref="UserNotFoundException">Se lanza cuando no existe el usuario.</exception>
        /// <exception cref="InvalidPasswordException">Se lanza cuando no coincide la contraseña introducida por el usuario</exception>
        /// <exception cref="Exception">Se lanza cuando falla el proceso de registro</exception>
        Task<Account> Login(string username, string password); 
    }
}
