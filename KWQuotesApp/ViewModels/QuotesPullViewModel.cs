using KWQuotesApp.Infrastructure.APIClient;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace KWQuotesApp.ViewModels
{
    public class QuotesPullViewModel : BindableBase, INavigationAware
    {
        public QuotesPullViewModel(IQuotesApiClient apiCilent)
        {
            Upload = new DelegateCommand(UploadQuotes, () => CanUploadQuotes)
                .ObservesProperty(() => Quantity);

            Analyse = new DelegateCommand(AnalyseSelected, () => CanAnalyseSelected)
                .ObservesProperty(() => SelectedQuote);

            Quotes = new ObservableCollection<string>();

            quotesApiCilent = apiCilent;
        }

        private IQuotesApiClient quotesApiCilent { get; set; }

        private string title = "Upload Quotes";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        private string errorText = "test error";
        public string ErrorText
        {
            get { return errorText; }
            set { SetProperty(ref errorText, value); }
        }

        private ObservableCollection<string> quotes;

        public ObservableCollection<string> Quotes
        {
            get { return quotes; }
            set { SetProperty(ref quotes, value); }
        }

        private string selectedQuote;
        public string SelectedQuote
        {
            get { return selectedQuote; }
            set { SetProperty(ref selectedQuote, value); }
        }

        public DelegateCommand Upload { get; set; } 

        public bool CanUploadQuotes
        {
            get
            {
                if(quantity < 10 || quantity > 20)
                {
                    ErrorText = "Quantity must be between 10 an 20";
                    return false;
                }

                ErrorText = "";
                return true;
            }
        }

        public DelegateCommand Analyse { get; set; }

        public void AnalyseSelected()
        {
            // TODO:
        }

        public bool CanAnalyseSelected
        {
            get
            {
                return selectedQuote != null;
            }
        }

        private void UploadQuotes()
        {
            for (int i = 0; i < Quantity; i++)
            {
                UploadSingleQuote();
            }
        }

        public async void UploadSingleQuote()
        {
            string quote;
            do
            {
                quote = await quotesApiCilent.GetQuote(ConfigurationManager.AppSettings["QuoteApiUrl"]);
            } while (quotes.Contains(quote));
            quotes.Add(quote);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        { }
    }
}
