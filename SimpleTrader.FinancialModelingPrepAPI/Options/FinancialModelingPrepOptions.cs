using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.FinancialModelingPrepAPI.Options
{
    public class FinancialModelingPrepOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ProfileEndpoint { get; set; } = string.Empty;
        public string QuoteEndpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
