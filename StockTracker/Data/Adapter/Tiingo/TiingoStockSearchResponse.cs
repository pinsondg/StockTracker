using System;
namespace StockTracker.Data.Adapter.Tiingo
{
    public class TiingoStockSearchResponse
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string AssetType { get; set; }
        public bool IsActive { get; set; }
        public string PermaTicker { get; set; }
        public string OpenFIGI { get; set; }

        public TiingoStockSearchResponse()
        {
        }
    }
}
