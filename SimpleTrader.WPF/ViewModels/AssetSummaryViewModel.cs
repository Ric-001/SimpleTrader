using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        
        public double AccountBalance => _assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new(assetStore, 3);
            _assetStore = assetStore;
            _assetStore.StateChanged += OnAssetStoreStateChanged;
        }

       

        private void OnAssetStoreStateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
           
        }
    }
}