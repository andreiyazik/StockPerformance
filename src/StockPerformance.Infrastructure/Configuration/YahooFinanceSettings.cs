namespace StockPerformance.Infrastructure.Configuration
{
    public class YahooFinanceSettings
    {
        public string ApiKey { get; set; }
        public string GetHistoryUrl { get; set; }
        public string XRapidApiHost { get; set; }
        public string StockHistoryRootNode { get; set; }
    }
}
