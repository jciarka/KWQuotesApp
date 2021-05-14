using KWQuotesApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<ValidatedQuote> ValidateSingleQuote(string quote, string url)
        {
            var jsonContent= JsonSerializer.Serialize(new { text = quote });
            HttpContent httpContent = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ValidatedQuote>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            };
        }

        public async Task<ValidatedQuotes> ValidateQuotes(IEnumerable<string> quotes, string url)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var quote in quotes)
            {
                stringBuilder.Append(quote + ".\n");
            }

            var jsonContent = JsonSerializer.Serialize(new { text = stringBuilder.ToString() });
            HttpContent httpContent = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync(url, httpContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ValidatedQuotes>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            };
        }
    }
}
