using System;
using System.Linq;

namespace StockPerformance.Infrastructure.ExtensionMethods
{
    public static class Date
    {
        private static readonly DateTime Epoch = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );

        private static readonly TimeZoneInfo TzEst = TimeZoneInfo
            .GetSystemTimeZones()
            .Single( tz => tz.Id == "Eastern Standard Time" || tz.Id == "America/New_York" );

        public static DateTime ToUtcFrom( this DateTime dt, TimeZoneInfo tzi )
        {
            return TimeZoneInfo.ConvertTimeToUtc( dt, tzi );
        }

        public static DateTime FromEstToUtc( this DateTime dt )
        {
            return DateTime.SpecifyKind( dt, DateTimeKind.Unspecified ).ToUtcFrom( TzEst );
        }

        public static string ToUnixTimestamp( this DateTime dt )
        {
            return DateTime.SpecifyKind( dt, DateTimeKind.Utc )
                .Subtract( Epoch )
                .TotalSeconds
                .ToString( "F0" );
        }
    }
}
