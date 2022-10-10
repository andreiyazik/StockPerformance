using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockPerformance.ExternalServices.Contracts
{
    public interface IStockService
    {
        Task<IEnumerable<CandleViewModel>> GetHistoryAsync( string symbol, EPeriod period, EGranularity granularity );
    }
}