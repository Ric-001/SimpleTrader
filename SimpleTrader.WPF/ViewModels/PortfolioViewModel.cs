using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
        private int? _maxAssets = null;  //Cambiar a un número específico si se desea limitar la cantidad de activos mostrados
        public AssetListingViewModel AssetListingViewModel { get;  }

        public PortfolioViewModel(AssetStore assetStore, Func<int?, AssetListingViewModel> assetListingFactory)
        {
            AssetListingViewModel = assetListingFactory(_maxAssets);
        }

    }
}
