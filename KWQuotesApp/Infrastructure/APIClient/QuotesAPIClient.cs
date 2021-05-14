using KWQuotesApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KWQuotesApp.Infrastructure.APIClient
{
    public class QuotesAPIClient : IQuotesApiClient
    {
        private HttpClient client;

        public QuotesAPIClient()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            client = new HttpClient();  
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<string> GetQuote(string url)
        {
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Quote>();
                    return result.quote;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            };
        }
    }
}
