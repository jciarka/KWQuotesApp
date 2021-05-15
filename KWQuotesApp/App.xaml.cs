using KWQuotesApp.Infrastructure.APIClient;
using KWQuotesApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace KWQuotesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IQuotesApiClient, QuotesAPIClient>();

            // te rejestracje są potrzebne dla przełączania widoków przez mechanzim RequestNavigate
            containerRegistry.RegisterForNavigation<QuotesPull>("QuotesPull");
            containerRegistry.RegisterForNavigation<QuoteAnalyse>("QuoteAnalyse");
            containerRegistry.RegisterForNavigation<QuotesSummary>("QuotesSummary");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
