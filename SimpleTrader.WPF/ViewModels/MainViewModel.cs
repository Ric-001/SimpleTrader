using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;
        private readonly IStockPriceService _stockService;
        public INavigator Navigator { get; set; } 

        public MainViewModel(IMajorIndexService majorIndexService, IStockPriceService stockService, INavigator navigator)
        {
            _majorIndexService = majorIndexService;
            _stockService = stockService;
            Navigator = navigator;
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
