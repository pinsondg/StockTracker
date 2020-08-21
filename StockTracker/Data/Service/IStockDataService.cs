using System;
using System.Collections.Generic;

namespace StockTracker.Data.Service
{
    public interface IStockDataService
    {

        public double GetCurrentStockPrice(string ticker);

        public double GetStockPriceForDate(string ticker);

        public List<string> SearchStocks(string query);

    }
}
