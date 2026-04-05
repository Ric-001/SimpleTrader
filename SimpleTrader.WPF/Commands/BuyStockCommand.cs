using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
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
        public BuyStockCommand(BuyViewModel viewModel, IBuyStockService buyStockService)
        {
            _viewModel = viewModel;
            _buyStockService = buyStockService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public async void Execute(object? parameter)
        {
            try
            {
                Account account = await _buyStockService.BuyStock(new Account()
                {
                    Id = 1, // Simulación de cuenta con ID 1
                    Balance = 500, // Simulación de balance inicial
                    AssetTransactions = new List<AssetTransaction>()
                }, _viewModel.Symbol, _viewModel.SharesToBuy);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al comprar acciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    } 
}
