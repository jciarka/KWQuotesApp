using KWQuotesApp.Configuration;
using KWQuotesApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace KWQuotesApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IRegionManager regionManager { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion(RegionsNames.MainRegion, typeof(QuotesPull));
        }
    }
}
