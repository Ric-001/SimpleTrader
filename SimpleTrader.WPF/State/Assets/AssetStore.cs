using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Assets
{
    public class AssetStore
    {
        public event Action StateChanged;

        private readonly IAccountStore _accountStore;

        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssetTransaction> AssetsTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? [];
        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.StateChanged += OnAccountStoreStateChanged;


        }

        private void OnAccountStoreStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
