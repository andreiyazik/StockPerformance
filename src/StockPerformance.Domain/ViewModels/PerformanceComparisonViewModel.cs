using System;
using System.Collections.Generic;
using System.Text;

namespace StockPerformance.Domain.ViewModels
{
    public class PerformanceComparisonViewModel
    {
        public List<string> Dates { get; set; }
        public List<PerformanceViewModel> PerformanceResults { get; set; }
    }
}
