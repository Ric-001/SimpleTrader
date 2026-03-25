namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public partial class MajorIndexService
    {
        // DTO interno que refleja el JSON de la API
        private class MajorIndexDto
        {
            public string Symbol { get; set; } = string.Empty;
            public double Price { get; set; }
            public double MarketCap { get; set; }
            public double Beta { get; set; }
            public double LastDividend { get; set; }
            public string Range { get; set; } = string.Empty;
            public double Change { get; set; }
            public double ChangePercentage { get; set; }

            // En el JSON vienen como 372.09026, 67955.07281, etc.
            public double Volume { get; set; }
            public double AverageVolume { get; set; }

            public string CompanyName { get; set; } = string.Empty;
            public string Currency { get; set; } = string.Empty;
            public string Cik { get; set; } = string.Empty;
            public string Isin { get; set; } = string.Empty;

            // Pueden ser null en el JSON
            public string? Cusip { get; set; }
            public string ExchangeFullName { get; set; } = string.Empty;
            public string Exchange { get; set; } = string.Empty;
            public string Industry { get; set; } = string.Empty;
            public string? Website { get; set; }

            public string Description { get; set; } = string.Empty;
            public string Ceo { get; set; } = string.Empty;
            public string Sector { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string FullTimeEmployees { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Zip { get; set; } = string.Empty;
            public string Image { get; set; } = string.Empty;
            public string IpoDate { get; set; } = string.Empty;
            public bool DefaultImage { get; set; }
            public bool IsEtf { get; set; }
            public bool IsActivelyTrading { get; set; }
            public bool IsAdr { get; set; }
            public bool IsFund { get; set; }
        }
    }
}
