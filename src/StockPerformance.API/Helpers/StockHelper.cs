using System;
using System.Collections.Generic;

namespace StockPerformance.API.Helpers
{
    internal static class StockHelper
    {
        public static List<double> CalculatePerformance( List<double> values )
        {
            var results = new List<double>();

            if(values.Count > 0)
            {
                // Treating first value as 100%
                var first = values[0];
                results.Add( 100 );

                for( var i = 1; i < values.Count; i++ )
                {
                    var percentage = ( ( values[i] - first ) * 100 ) / first;
                    results.Add( 100 + Math.Round( percentage, 2 ) );
                }
            }

            return results;
        }
    }
}