using KWQuotesApp.Configuration;
using KWQuotesApp.Infrastructure.APIClient;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KWQuotesApp.ViewModels
{
    public class QuoteAnalyseViewModel : BindableBase, INavigationAware
    {
        private readonly IQuotesApiClient quotesApiClient;
        private readonly IRegionManager regionManager;

        private string quote;
        public string Quote
        {
            get { return quote; }
            set { SetProperty(ref quote, value); }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        private float polarity;
        public float Polarity
        {
            get { return polarity; }
            set { SetProperty(ref polarity, value); }
        }

        private string title = "Quote analyse";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string errorText = "";
        public string ErrorText
        {
            get { return errorText; }
            set { SetProperty(ref errorText, value); }
        }

        public QuoteAnalyseViewModel(IRegionManager regionManager, IQuotesApiClient quotesApiClient)
        {
            this.quotesApiClient = quotesApiClient;
            this.regionManager = regionManager;
            GoBack = new DelegateCommand(navigateBack);
        }

        public DelegateCommand GoBack { get; set; }

        private void navigateBack()
        {
            regionManager.RequestNavigate(RegionsNames.MainRegion, "QuotesPull");
        }


        public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Quote = (string)navigationContext.Parameters["quote"];

            AnalyseQuote(Quote);
        }

        private async void AnalyseQuote(string quote)
        {
            ErrorText = "";
            try
            {
                var result = await quotesApiClient.ValidateSingleQuote(quote, ConfigurationManager.AppSettings["QuoteValidatorApiUrl"]);

                Polarity = result.result.polarity;
                Type = result.result.type;
            }
            catch(Exception e)
            {
                ErrorText = "Connection error. Try again later.";
            }

        }
    }
}
