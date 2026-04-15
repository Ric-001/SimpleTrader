using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        private string _symbol = string.Empty;
        private string _searchResultSymbol = string.Empty;
        private double _stockPrice = 0;
        private int _sharesToBuy = 0;
        private string _errorMessage = string.Empty;

        public string Symbol { get => _symbol; set => SetProperty(ref _symbol, value); }
        public string SearchResultSymbol { get => _searchResultSymbol; set => SetProperty(ref _searchResultSymbol, value); }
        public double StockPrice { get => _stockPrice; set { SetProperty(ref _stockPrice, value); OnPropertyChanged(nameof(TotalPrice)); } }
        public int SharesToBuy { get => _sharesToBuy; set { SetProperty(ref _sharesToBuy, value); OnPropertyChanged(nameof(TotalPrice)); } }
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }
        public double TotalPrice => StockPrice * SharesToBuy;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }
    }
}
