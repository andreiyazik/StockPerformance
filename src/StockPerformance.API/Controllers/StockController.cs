using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockPerformance.API.Features;
using StockPerformance.Domain.Enums;
using StockPerformance.Domain.ViewModels;
using StockPerformance.Infrastructure.Controllers;
using System;
using System.Threading.Tasks;

namespace StockPerformance.API.Controllers
{
    [Route( "stock" )]
    public class StockController : BaseController
    {
        private readonly IConfiguration _configuration;

        public StockController( IMediator mediator, IConfiguration configuration )
            : base( mediator )
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("performance-comparison")]
        public async Task<BaseResponse> GetPerformanceComparison( string symbol, EPeriod period, EGranularity granularity )
        {
            var symbolToCompare = _configuration["SymbolToCompare"];

            var query = new GetPerformanceComparisonQuery( symbol, symbolToCompare, period, granularity );

            try
            {
                var result = await _mediator.Send( query );
                return new SuccessResponse<PerformanceComparisonViewModel>( result );
            }
            catch( Exception ex )
            {
                return new ErrorResponse<GetPerformanceComparisonQuery>( ex, query );
            }
        }
    }
}