using System;
using System.Collections.Generic;
using StockTracker.Data.Adapter.Tiingo;

namespace StockTracker.Data.Service
{
    public interface IStockDataService
    {

        public TiingoCurrentTopOfBookResponse GetCurrentStockData(string ticker);

        public double GetStockPriceForDate(string ticker, DateTime date);

        public List<TiingoStockSearchResponse> SearchStocks(string query);

    }
}
