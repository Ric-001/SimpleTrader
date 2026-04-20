using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private const int MaxAssets = 3;
        private readonly AssetStore _assetStore;
        
        public double AccountBalance => _assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore, Func<int?, AssetListingViewModel> assetListingFactory)
        {
            AssetListingViewModel = assetListingFactory(MaxAssets);
            _assetStore = assetStore;
            _assetStore.StateChanged += OnAssetStoreStateChanged;
        }

        private void OnAssetStoreStateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
           
        }
    }
}