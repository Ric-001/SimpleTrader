using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.Factories
{
    public interface IRootTraderViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
    
