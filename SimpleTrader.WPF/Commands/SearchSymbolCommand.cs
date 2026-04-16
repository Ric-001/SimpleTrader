using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    internal class SearchSymbolCommand : AsyncCommandBase
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
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter) =>
            !IsExecuting && !string.IsNullOrWhiteSpace(_viewModel.Symbol);

        protected override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            double stockPrice = await _stockPriceService.GetPrice(_viewModel.Symbol);
            _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
            _viewModel.StockPrice = stockPrice;
        }

        protected override void OnExecutionFailed(Exception ex)
        {
            _viewModel.ErrorMessage = ex switch
            {
                InvalidSymbolException => "Símbolo de acción no válido. Por favor, verifica el símbolo.",
                _ => $"Error al obtener el precio de la acción: {ex.Message}"
            };
        }
    }


}