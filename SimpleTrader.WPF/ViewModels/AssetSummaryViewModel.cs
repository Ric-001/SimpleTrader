using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;

        public double AccountBalance => _assetStore.AccountBalance;
        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();
            _assetStore.StateChanged += OnAssetStoreStateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetsTransactions
                .GroupBy(at => at.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(g=>g.Shares>0);

            _assets.Clear();

            foreach (var assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }

        private void OnAssetStoreStateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }
    }
}