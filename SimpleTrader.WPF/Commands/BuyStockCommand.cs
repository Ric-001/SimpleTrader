using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    internal class BuyStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
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

        public bool CanExecute(object? parameter)
        {
            return _accountStore.CurrentAccount != null;
        }

        public async void Execute(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            Account ? currentAccount = _accountStore.CurrentAccount;
            if (currentAccount == null) return;
            
            try
            {
                Account? account = await _buyStockService.BuyStock(currentAccount, _viewModel.Symbol, _viewModel.SharesToBuy);
                _accountStore.CurrentAccount = account;

                _viewModel.StatusMessage = $"Compra realizada {_viewModel.SharesToBuy} acciones de {_viewModel.Symbol} por un total de {_viewModel.TotalPrice:N2}€.";
            }
            catch (InsufficientFundsException)
            {
                _viewModel.ErrorMessage = $"Fondos insuficientes. Saldo disponible: {currentAccount.Balance:N2}€, Precio de la transacción: {_viewModel.TotalPrice:N2}€.";
            }
            catch (InvalidSymbolException) 
            {
                _viewModel.ErrorMessage = "Símbolo de acción no válido. Por favor, verifica el símbolo ingresado.";
            }

            catch (Exception ex)
            {
                _viewModel.ErrorMessage = ex.Message;
            }
        }
    }
}
