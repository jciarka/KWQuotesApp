using KWQuotesApp.Configuration;
using KWQuotesApp.Infrastructure.APIClient;
using KWQuotesApp.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace KWQuotesApp.ViewModels
{
    public class QuotesSummaryViewModel : BindableBase, INavigationAware
    {
        private IEnumerable<string> quotes;

        private readonly IRegionManager regionManager;
        private readonly IContainerProvider container;

        private string title = "Summary";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private int positiveCount;
        public int PositiveCount
        {
            get { return positiveCount; }
            set { SetProperty(ref positiveCount, value); }
        }

        private string mostPositive;
        public string MostPositive
        {
            get { return mostPositive; }
            set { SetProperty(ref mostPositive, value); }
        }

        private float mostPositivePolarity;
        public float MostPositivePolarity
        {
            get { return mostPositivePolarity; }
            set { SetProperty(ref mostPositivePolarity, value); }
        }

        private int negativeCount;
        public int NegativeCount
        {
            get { return negativeCount; }
            set { SetProperty(ref negativeCount, value); }
        }

        private string mostNegative;
        public string MostNegative
        {
            get { return mostNegative; }
            set { SetProperty(ref mostNegative, value); }
        }

        private float mostNegativePolarity;
        public float MostNegativePolarity
        {
            get { return mostNegativePolarity; }
            set { SetProperty(ref mostNegativePolarity, value); }
        }

        private float neutralCount;
        public float NeutralCount
        {
            get { return neutralCount; }
            set { SetProperty(ref neutralCount, value); }
        }

        public DelegateCommand GoBack { get; set; }

        public QuotesSummaryViewModel(IRegionManager regionManager, IContainerProvider container)
        {
            GoBack = new DelegateCommand(navigateBack);

            this.regionManager = regionManager;
            this.container = container;
        }

        private void navigateBack()
        {
            regionManager.RequestNavigate(RegionsNames.MainRegion, "QuotesPull");
        }


        public bool IsNavigationTarget(NavigationContext navigationContext) => true;


        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          
        }

        public void OnNavigatedTo(NavigationContext navigationContext) 
        {
            quotes = (IEnumerable<string>)navigationContext.Parameters["quotes"];
            AnalyseQuotes();
        }

        private async void AnalyseQuotes()
        {
            
            List<Tuple<string, ValidatedQuote>> validatedQuotes = new List<Tuple<string, ValidatedQuote>>();
            List<Task<Tuple<string, ValidatedQuote>>> validationOperations = new List<Task<Tuple<string, ValidatedQuote>>>();
            foreach(var quote in quotes)
            {
                Task<Tuple<string, ValidatedQuote>> task = Task<Tuple<string, ValidatedQuote>>.Run(() => getValidationResult(quote));
                validationOperations.Add(task);
            }

            foreach(var task in validationOperations)
            {
                var result = await task;
                validatedQuotes.Add(result);
            }

            PositiveCount = validatedQuotes.Count(x => x.Item2.result.polarity > 0);
            NegativeCount = validatedQuotes.Count(x => x.Item2.result.polarity < 0);
            NeutralCount = validatedQuotes.Count(x => x.Item2.result.polarity == 0);

            MostPositivePolarity = validatedQuotes.Max(x => x.Item2.result.polarity);
            MostPositive = validatedQuotes.Find(x => x.Item2.result.polarity == mostPositivePolarity).Item1;
            MostNegativePolarity = validatedQuotes.Min(x => x.Item2.result.polarity);
            MostNegative= validatedQuotes.Find(x => x.Item2.result.polarity == MostNegativePolarity).Item1;
            
        }

        private async Task<Tuple<string, ValidatedQuote>> getValidationResult(string quote)
        {
            IQuotesApiClient apiClient = container.Resolve<IQuotesApiClient>();
            Tuple<string, ValidatedQuote> result = new Tuple<string, ValidatedQuote>(quote,
                await apiClient.ValidateSingleQuote(quote, ConfigurationManager.AppSettings["QuoteValidatorApiUrl"]));
            return result;
        }
    }
}
