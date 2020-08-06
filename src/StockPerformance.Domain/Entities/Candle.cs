using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPerformance.Domain.Entities
{
    [Table("Candles")]
    public class Candle : IEntity
    {
        public string Symbol { get; set; }

        public long Timestamp { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public long Volume { get; set; }

        public decimal? AdjustedClose { get; set; }
    }
}