using SimpleTrader.Domain.Models;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public event Action? StateChanged;

        private Account? _currentAccount;
        public Account? CurrentAccount 
        { 
            get => _currentAccount; 
            set  { _currentAccount = value;  StateChanged?.Invoke(); }
        }

    }
}
