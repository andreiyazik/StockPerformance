using MediatR;
using Microsoft.Extensions.Configuration;
using StockPerformance.API.Features;
using StockPerformance.Domain.ViewModels;
using StockPerformance.ExternalServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockPerformance.API.Handlers
{
    public class GetHistoryDataQueryHandler : IRequestHandler<GetHistoryDataQuery, PerformanceComparisonViewModel>
    {
        private readonly IStockService _stockService;
        private readonly IConfiguration _configuration;

        public GetHistoryDataQueryHandler( IStockService stockService, IConfiguration configuration )
        {
            _stockService = stockService;
            _configuration = configuration;
        }

        public async Task<PerformanceComparisonViewModel> Handle( GetHistoryDataQuery request, CancellationToken cancellationToken )
        {
            try
            {
                var symbolToCompare = _configuration["SymbolToCompare"];

                var symbolResult = await _stockService.GetHistoryAsync( request.Symbol, request.Period );
                var symbolToCompareResult = await _stockService.GetHistoryAsync( symbolToCompare, request.Period );

                var symbolPerformance = symbolResult.Select( s => Double.Parse( s.Close.ToString() ) ).ToList();
                var symbolToComparePerformance = symbolToCompareResult.Select( s => Double.Parse( s.Close.ToString() ) ).ToList();

                var result = new PerformanceComparisonViewModel
                {
                    Dates = symbolResult.Select( s => s.Date.ToString() ).ToList(),
                    PerformanceResults = new List<PerformanceViewModel>()
                };

                result.PerformanceResults.Add( new PerformanceViewModel
                {
                    Symbol = request.Symbol,
                    Results = symbolPerformance
                } );
                result.PerformanceResults.Add( new PerformanceViewModel
                {
                    Symbol = symbolToCompare,
                    Results = symbolToComparePerformance
                } );

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
