namespace SimpleTrader.FinancialModelingPrepAPI.Services
{

    // DTO interno que refleja el JSON de la API
    public class MajorIndexDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double ChangePercentage { get; set; }
        public double Change { get; set; }
        public double Volume { get; set; }
        public double DayLow { get; set; }
        public double DayHigh { get; set; }
        public double YearHigh { get; set; }
        public double YearLow { get; set; }
        public double MarketCap { get; set; }
        public double PriceAvg50 { get; set; }
        public double PriceAvg200 { get; set; }
        public string Exchange { get; set; } = string.Empty;
        public double Open { get; set; }
        public double PreviousClose { get; set; }
        public long Timestamp { get; set; }
    }

}
