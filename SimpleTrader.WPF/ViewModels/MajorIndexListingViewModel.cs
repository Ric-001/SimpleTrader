using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private IMajorIndexService _majorIndexService;
        private MajorIndex _dowJones = new MajorIndex();
        private MajorIndex _nasdaq = new MajorIndex();
        private MajorIndex _sp500 = new MajorIndex();

        public MajorIndex DowJones { get => _dowJones; set => SetProperty(ref _dowJones, value); }
        public MajorIndex Nasdaq { get => _nasdaq; set => SetProperty(ref _nasdaq, value); }
        public MajorIndex SP500 { get => _sp500; set => SetProperty(ref _sp500, value); }


        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexListingViewModel = new MajorIndexListingViewModel(majorIndexService);
            majorIndexListingViewModel.LoadMajorIndexes();
            return majorIndexListingViewModel;
        }

        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if (task.Exception == null)
                    DowJones = task.Result;
            });

            _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception == null)
                    Nasdaq = task.Result;
            });

            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null)
                    SP500 = task.Result;
            });
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}