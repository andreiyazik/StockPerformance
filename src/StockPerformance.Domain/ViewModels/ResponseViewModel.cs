using StockPerformance.Domain.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace StockPerformance.Domain.ViewModels
{
    public class BaseResponse
    {
    }

    public class SuccessResponse<T> : BaseResponse
    {
        public SuccessResponse( T response )
        {
            Response = response;
        }

        public T Response { get; set; }
    }

    public class ErrorResponse<T> : BaseResponse
    {
        public ErrorResponse( Exception exception, T request_params )
        {
            Error = new Error<T>()
            {
                ErrorMessage = exception.Message,
                RequestParams = request_params.GetProperties()
            };
        }

        public Error<T> Error { get; set; }
    }

    public class Error<T>
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<KeyValuePair<string, object>> RequestParams { get; set; }
    }
}