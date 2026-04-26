using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        public override event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;
        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }
        public override bool CanExecute(object? parameter) => _accountStore.CurrentAccount != null;
        protected override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;
            Account currentAccount = _accountStore.CurrentAccount!;
            Account updated = await _sellStockService.SellStock(currentAccount, _viewModel.Symbol, _viewModel.SharesToTransfer);
            _accountStore.CurrentAccount = updated;
            _viewModel.SearchResultSymbol = string.Empty;
            _viewModel.StatusMessage = $"Venta realizada: {_viewModel.SharesToTransfer} acciones de {_viewModel.Symbol} por {_viewModel.StockPrice * _viewModel.SharesToTransfer:N2}€.";
        }
        protected override void OnExecutionFailed(Exception ex)
        {
            _viewModel.ErrorMessage = ex switch
            {
                InsufficientSharesException e => $"No tienes suficientes acciones para vender. Tienes: {e.AccountShares}, intentaste vender: {e.RequiredShares}.",
                InvalidSymbolException => "Símbolo no válido. Verifica el símbolo ingresado.",
                _ => ex.Message
            };
        }
    }
}