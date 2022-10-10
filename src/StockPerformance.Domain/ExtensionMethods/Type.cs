using System.Collections.Generic;

namespace StockPerformance.Domain.ExtensionMethods
{
    public static class Type
    {
        public static List<KeyValuePair<string, object>> GetProperties( this object obj )
        {
            var result = new List<KeyValuePair<string, object>>();

            foreach (var property in obj.GetType().GetProperties())
            {
                result.Add( new KeyValuePair<string, object>( property.Name, property.GetValue( obj ) ) );
            }

            return result;
        }
    }
}