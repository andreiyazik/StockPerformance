using Microsoft.Extensions.Options;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;
using StockPerformance.ExternalServices.Contracts;
using StockPerformance.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockPerformance.ExternalServices.AlphaVantage
{
    public class AlphaVantageStockService : IStockService
    {
        private readonly IOptions<AlphaVantageSettings> _alpha_vantage_settings;

        public AlphaVantageStockService( IOptions<AlphaVantageSettings> alpha_vantage_settings )
        {
            _alpha_vantage_settings = alpha_vantage_settings;
        }

        public Task<IEnumerable<CandleViewModel>> GetHistoryAsync( string symbol, EPeriod period )
        {
            throw new NotImplementedException();
        }
    }
}
