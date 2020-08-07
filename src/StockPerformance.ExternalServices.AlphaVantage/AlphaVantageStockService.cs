using Microsoft.Extensions.Options;
using ServiceStack;
using ServiceStack.Text;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;
using StockPerformance.ExternalServices.Contracts;
using StockPerformance.ExternalServices.Contracts.Models;
using StockPerformance.Infrastructure.Configuration;
using StockPerformance.Infrastructure.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CandleViewModel>> GetHistoryAsync( string symbol, EPeriod period, EGranularity granularity )
        {
            try
            {
                string function = granularity == EGranularity.Standard ? "TIME_SERIES_DAILY" : "TIME_SERIES_INTRADAY";
                string interval = granularity == EGranularity.Standard ? "&interval=60min" : string.Empty;
                var response = await string.Format( _alpha_vantage_settings.Value.GetHistoryUrl, function, symbol, _alpha_vantage_settings.Value.ApiKey, interval )
                                .GetStringFromUrlAsync();
                var formattedResult = response.FromCsv<List<StockHistoryResponse>>();

                var startDate = period == EPeriod.Day ? DateTime.Now.AddDays( 1 ).Date
                    : period == EPeriod.Week ? DateTime.Now.FirstDayOfWeek()
                    : DateTime.Now.FirstDayOfMonth();

                var result = formattedResult.Where( r => r.Timestamp >= startDate )
                    .Select( r => new CandleViewModel
                    {
                        Date = long.Parse( r.Timestamp.ToUnixTimestamp() ),
                        Open = r.Open,
                        Close = r.Close,
                        High = r.High,
                        Low = r.Low,
                        Volume = r.Volume
                    } ).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception( "Incorrect symbol provided, or error loading data." );
            }
        }
    }
}