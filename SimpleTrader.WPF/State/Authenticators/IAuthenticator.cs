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
        /// <summary>
        /// Realiza el proceso de inicio de sesión para un usuario utilizando su correo electrónico y contraseña. Este método es asíncrono y puede lanzar excepciones si las credenciales son incorrectas o si ocurre algún error durante el proceso de autenticación.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// 
        Task Login(string email, string password);
        void Logout();
    }
}
