using Newtonsoft.Json;
using StockPerformance.Domain.Entities;
using System;

namespace StockPerformance.Domain.ViewModels
{
    public class CandleViewModel
    {
        public CandleViewModel()
        {
        }

        public CandleViewModel(Candle candle)
        {
            Date = candle.Timestamp;
            Open = candle.Open;
            High = candle.High;
            Low = candle.Low;
            Close = candle.Close;
            Volume = candle.Volume;
            AdjustedClose = candle.AdjustedClose;
        }

        [JsonProperty( "date" )]
        public long Date { get; internal set; }

        [JsonProperty( "open" )]
        public decimal Open { get; internal set; }

        [JsonProperty( "high" )]
        public decimal High { get; internal set; }

        [JsonProperty( "low" )]
        public decimal Low { get; internal set; }

        [JsonProperty( "close" )]
        public decimal Close { get; internal set; }

        [JsonProperty( "volume" )]
        public long Volume { get; internal set; }

        [JsonProperty( "adjclose" )]
        public decimal? AdjustedClose { get; internal set; }

        public Candle CreateEntity( string symbol )
        {
            return new Candle
            {
                Symbol = symbol,
                Timestamp = Date,
                Open = Open,
                High = High,
                Low = Low,
                Close = Close,
                Volume = Volume,
                AdjustedClose = AdjustedClose
            };
        }
    }
}