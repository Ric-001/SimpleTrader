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
        public event EventHandler? CanExecuteChanged;

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
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, _viewModel.Symbol, _viewModel.SharesToBuy);
                _accountStore.CurrentAccount = account;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al comprar acciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    } 
}
