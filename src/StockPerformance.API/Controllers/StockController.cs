using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public StockController( IMediator mediator )
            : base( mediator )
        {
        }

        [HttpGet]
        [Route("performance-comparison")]
        public async Task<BaseResponse> GetPerformanceComparison( string symbol = "", EPeriod period = EPeriod.Week )
        {
            var query = new GetHistoryDataQuery( symbol, period );

            try
            {
                var result = await _mediator.Send( query );
                return new SuccessResponse<PerformanceComparisonViewModel>( result );
            }
            catch( Exception ex )
            {
                return new ErrorResponse<GetHistoryDataQuery>( ex, query );
            }
        }
    }
}