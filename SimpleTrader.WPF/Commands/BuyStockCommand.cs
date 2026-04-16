using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : AsyncCommandBase
    {
        // Sobreescribe el evento para delegar en CommandManager,
        public override event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly BuyViewModel _viewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel viewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public override bool CanExecute(object? parameter) => _accountStore.CurrentAccount != null;

        protected override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            Account currentAccount = _accountStore.CurrentAccount!;

            Account updated = await _buyStockService.BuyStock(
                currentAccount, _viewModel.Symbol, _viewModel.SharesToBuy);

            _accountStore.CurrentAccount = updated;
            _viewModel.StatusMessage =
                $"Compra realizada: {_viewModel.SharesToBuy} acciones de {_viewModel.Symbol} " +
                $"por {_viewModel.TotalPrice:N2}€.";
        }

        protected override void OnExecutionFailed(Exception ex)
        {
            _viewModel.ErrorMessage = ex switch
            {
                InsufficientFundsException e =>$"Fondos insuficientes. Saldo: {e.AccountBalance:N2}€, necesitas: {e.RequiredBalance:N2}€.",
                InvalidSymbolException => "Símbolo no válido. Verifica el símbolo ingresado.",
                _ => ex.Message
            };
        }
    }
}