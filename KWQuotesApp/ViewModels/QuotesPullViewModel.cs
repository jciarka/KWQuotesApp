using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KWQuotesApp.ViewModels
{
    public class QuotesPullViewModel : BindableBase, INavigationAware
    {
        public QuotesPullViewModel()
        {
            Upload = new DelegateCommand(UploadQuotes, () => CanUploadQuotes)
                .ObservesProperty(() => Quantity);

            Analyse = new DelegateCommand(AnalyseSelected, () => CanAnalyseSelected);

            Quotes = new ObservableCollection<string>()
            {
                "abc",
                "abc",
                "abc",
                "abc",
                "abc",
                "abc"
            };
        }

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

        public void UploadQuotes()
        {

        }

        public bool CanUploadQuotes
        {
            get
            {
                if(quantity < 10 || quantity > 20)
                {
                    errorText = "Quantity must be between 10 an 20";
                    return false;
                }
                errorText = "";
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




        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        { }
    }
}
