using MediatR;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;
using System.Collections;
using System.Collections.Generic;

namespace StockPerformance.API.Features
{
    public class GetHistoryDataQuery : IRequest<PerformanceComparisonViewModel>
    {
        public string Symbol { get; private set; }
        public EPeriod Period { get; private set; }
        public EGranularity Granularity { get; private set; }

        public GetHistoryDataQuery(string symbol, EPeriod period, EGranularity granularity)
        {
            Symbol = symbol;
            Period = period;
            Granularity = granularity;
        }
    }
}