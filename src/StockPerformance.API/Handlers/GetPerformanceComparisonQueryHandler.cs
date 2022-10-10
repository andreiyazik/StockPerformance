using MediatR;
using StockPerformance.API.Features;
using StockPerformance.API.Helpers;
using StockPerformance.API.Validators;
using StockPerformance.Domain.ViewModels;
using StockPerformance.ExternalServices.Contracts;
using StockPerformance.Persistence.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockPerformance.API.Handlers
{
    public class GetPerformanceComparisonQueryHandler : IRequestHandler<GetPerformanceComparisonQuery, PerformanceComparisonViewModel>
    {
        private readonly IStockService _stockService;
        private readonly ICandleRepository _candleRepository;

        public GetPerformanceComparisonQueryHandler( IStockService stockService, ICandleRepository candleRepository )
        {
            _stockService = stockService;
            _candleRepository = candleRepository;
        }

        public async Task<PerformanceComparisonViewModel> Handle( GetPerformanceComparisonQuery request, CancellationToken cancellationToken )
        {
            try
            {
                ValidateSymbol( request );

                var symbolResult = await _stockService.GetHistoryAsync( request.Symbol, request.Period, request.Granularity );
                var symbolToCompareResult = await _stockService.GetHistoryAsync( request.SymbolToCompare, request.Period, request.Granularity );

                await _candleRepository.BulkInsertOrUpdateAsync( symbolToCompareResult.Select( s => s.CreateEntity( request.Symbol ) ).ToList() );

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
                    Results = StockHelper.CalculatePerformance(symbolPerformance)
                } );
                result.PerformanceResults.Add( new PerformanceViewModel
                {
                    Symbol = request.SymbolToCompare,
                    Results = StockHelper.CalculatePerformance( symbolToComparePerformance )
                } );

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void ValidateSymbol( GetPerformanceComparisonQuery request )
        {
            var validator = new StockSymbolValidator();
            var validationResult = validator.Validate( request.Symbol );
            if (validationResult.Errors.Any())
            {
                throw new Exception( string.Join( ';', validationResult.Errors.Select( e => e.ErrorMessage ) ) );
            }
        }
    }
}
