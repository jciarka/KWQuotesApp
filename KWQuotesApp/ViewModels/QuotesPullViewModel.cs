using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KWQuotesApp.ViewModels
{
    public class QuotesPullViewModel : BindableBase, INavigationAware
    {
        private string _message = "Pull Quotes";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        /// Jeśli zwrócimy true (domyślne) -> widok i widok modelu będą singieltonem - podczas nawigacji jeśli istnieje instancja to zostanie wykorzystana
        /// Jeśłi zwrócimy false -> widok i widok modelu będą per request - zawsze nowa instancja 
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
