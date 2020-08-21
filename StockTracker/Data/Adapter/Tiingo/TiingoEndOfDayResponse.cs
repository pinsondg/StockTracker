using System;
namespace StockTracker.Data.Adapter.Tiingo
{
    public class TiingoEndOfDayResponse
    {
        public DateTime Date { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public Int64 Volume { get; set; }
        public float DivCash { get; set; }

        public TiingoEndOfDayResponse()
        {
        }
    }
}
