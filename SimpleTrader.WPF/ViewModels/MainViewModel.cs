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
        public INavigator Navigator { get; set; } = new Navigator();

        public MainViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
