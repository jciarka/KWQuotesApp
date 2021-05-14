using KWQuotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWQuotesApp.Infrastructure.APIClient
{
    public interface IQuotesApiClient
    {
        Task<string> GetQuote(string url);
        Task<ValidatedQuote> ValidateSingleQuote(string quote, string url);
        Task<ValidatedQuotes> ValidateQuotes(IEnumerable<string> quotes, string url);
    }
}
