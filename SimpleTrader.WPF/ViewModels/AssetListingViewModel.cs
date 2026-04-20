using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.ViewModels
{
    public class  AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;
        private readonly int? _maxNumberAssets;

        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore, int? maxNumberAssets = null)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();
            _maxNumberAssets = maxNumberAssets;
            _assetStore.StateChanged += OnAssetStoreStateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetsTransactions
                .GroupBy(at => at.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(g => g.Shares > 0)
                .OrderByDescending(a => a.Shares);

            if (_maxNumberAssets.HasValue)
                assetViewModels = assetViewModels.Take(_maxNumberAssets.Value);
            

            _assets.Clear();

            foreach (var assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }

        private void OnAssetStoreStateChanged()
        {
            ResetAssets();
        }
    }
}
