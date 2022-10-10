using System.Collections.Generic;

namespace StockPerformance.Domain.ViewModels
{
    public class PerformanceViewModel
    {
        public string Symbol { get; set; }
        public List<double> Results { get; set; }
    }
}
