using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        event Action StateChanged;
        Account? CurrentAccount { get; set; }
    }
}
