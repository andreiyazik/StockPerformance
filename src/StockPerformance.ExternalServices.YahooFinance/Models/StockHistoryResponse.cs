using Newtonsoft.Json;
using StockPerformance.Domain.ViewModels;
using System.Collections.Generic;

namespace StockPerformance.ExternalServices.YahooFinance.Models
{
    public class StockHistoryResponse
    {
        [JsonProperty( "prices" )]
        public IEnumerable<CandleViewModel> Prices { get; set; }
    }
}
