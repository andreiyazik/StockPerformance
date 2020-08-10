using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ExtensionMethods;
using StockPerformance.Domain.ViewModels;
using StockPerformance.ExternalServices.Contracts;
using StockPerformance.ExternalServices.YahooFinance.Models;
using StockPerformance.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPerformance.ExternalServices.YahooFinance
{
    public class YahooStockService : IStockService
    {
        private readonly IOptions<YahooFinanceSettings> _yahoo_finance_settings;

        public YahooStockService( IOptions<YahooFinanceSettings> yahoo_finance_settings )
        {
            _yahoo_finance_settings = yahoo_finance_settings;
        }

        public async Task<IEnumerable<CandleViewModel>> GetHistoryAsync( string symbol, 
            EPeriod period, 
            /** Intraday is not supported by yahoo Finance API **/
            EGranularity granularity )
        {
            try
            {
                var endDate = DateTime.UtcNow;
                var startDate = period == EPeriod.Day ? endDate.AddDays( -1 ) :
                    period == EPeriod.Week ? endDate.AddDays( -6 ).Date : endDate.AddMonths( -1 );

                var client = new RestClient( string.Format( _yahoo_finance_settings.Value.GetHistoryUrl, startDate.ToUnixTimestamp(), endDate.ToUnixTimestamp(), symbol ) );
                var request = new RestRequest( Method.GET );
                request.AddHeader( "x-rapidapi-host", _yahoo_finance_settings.Value.XRapidApiHost );
                request.AddHeader( "x-rapidapi-key", _yahoo_finance_settings.Value.ApiKey );
                var response = await client.ExecuteAsync( request );

                var result = JsonConvert.DeserializeObject<StockHistoryResponse>( response.Content );
                return result.Prices.OrderBy(r => r.Date).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception( "Incorrect symbol provided, or error loading data." );
            }
        }
    }
}