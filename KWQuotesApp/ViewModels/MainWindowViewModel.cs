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

        public DelegateCommand<string> NavigateCommand { get; set; }

        public IRegionManager regionManager { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.NavigateCommand = new DelegateCommand<string>(Navigate);

            // If you want ViewA to apear imediately after iniziation of window 
            // RequestNavigate WILL NOT WORK
            // use RegisterViewWithRegion instead (ONLY at initiation)
            regionManager.RegisterViewWithRegion("ContentRegion1", typeof(QuotesPull));
        }

        private void Navigate(string uri)
        {
            if (uri != null)
            {

                regionManager.RequestNavigate("ContentRegion1", uri, /* For test purposes*/(NavigationResult nr) =>
                {
                    var error = nr.Error;
                    var result = nr.Result;
                    // put a breakpoint here and checkout what NavigationResult contains
                });
            }
        }
    }
}
