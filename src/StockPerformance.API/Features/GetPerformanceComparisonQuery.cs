using MediatR;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;

namespace StockPerformance.API.Features
{
    public class GetPerformanceComparisonQuery : IRequest<PerformanceComparisonViewModel>
    {
        public string Symbol { get; private set; }
        public string SymbolToCompare { get; private set; }
        public EPeriod Period { get; private set; }
        public EGranularity Granularity { get; private set; }

        public GetPerformanceComparisonQuery(string symbol, string symbolToCompare, EPeriod period, EGranularity granularity)
        {
            Symbol = symbol;
            SymbolToCompare = symbolToCompare;
            Period = period;
            Granularity = granularity;
        }
    }
}