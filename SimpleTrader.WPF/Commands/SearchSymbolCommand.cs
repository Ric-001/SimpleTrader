using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    internal class SearchSymbolCommand : ICommand
    {
        private readonly BuyViewModel _viewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _viewModel = buyViewModel;
            _stockPriceService = stockPriceService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuyViewModel.Symbol))
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;



        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Symbol);
        }

        public async void Execute(object? parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_viewModel.Symbol);
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                _viewModel.StockPrice = stockPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al obtener el precio de la acción: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}