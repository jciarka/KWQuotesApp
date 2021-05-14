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
        
    }
}
