using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWQuotesApp.Models
{
    public class ValidatedQuotes
    {
        public ValidatedQuote result { get; set; }
        public List<ValidatedQuote> sentences { get; set; }
    }
}
