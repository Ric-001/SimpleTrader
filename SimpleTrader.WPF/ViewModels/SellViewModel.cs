using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private string _symbol = string.Empty;
        private string _searchResultSymbol = string.Empty;
        private double _stockPrice = 0;
        private int _sharesToTransfer = 0;
        private AssetViewModel _selectedAsset;

        public AssetListingViewModel AssetListingViewModel { get; }

        public string Symbol { get => _symbol; set => SetProperty(ref _symbol, value); }
        public string SearchResultSymbol { get => _searchResultSymbol; set => SetProperty(ref _searchResultSymbol, value); }
        public double StockPrice { get => _stockPrice; set => SetProperty(ref _stockPrice, value, [nameof(TotalPrice)]); }
        public int SharesToTransfer { get => _sharesToTransfer; set => SetProperty(ref _sharesToTransfer, value, [nameof(TotalPrice)]); }
        public AssetViewModel SelectedAsset { get => _selectedAsset; set => SetProperty(ref _selectedAsset, value); }

        public double TotalPrice => StockPrice * SharesToTransfer;
        public MessageViewModel ErrorMessageViewModel { get; } = new();
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }
        public MessageViewModel StatusMessageViewModel { get; } = new();
        public string StatusMessage { set => StatusMessageViewModel.Message = value; }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand SellStockCommand { get; set; }


        public SellViewModel(AssetStore assetStore, IStockPriceService stockPriceService, ISellStockService sellStockService, IAccountStore accountStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            
            //SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);
        }

    }
}