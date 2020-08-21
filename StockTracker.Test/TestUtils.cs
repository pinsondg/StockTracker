using System;
using System.Collections.Generic;
using NUnit.Framework;
using StockTracker.Data.Model;

namespace StockTracker.Test
{
    public class TestUtils
    {
        public TestUtils()
        {
        }


        public Position GetPositionCoveredCallSold_BoughtBack()
        {
            Security testOption = new Option()
            {
                SecurityId = 2,
                SecurityStatus = "",
                Count = 1,
                AveragePrice = 0.10
            };
            Security testStock = new Stock()
            {
                SecurityId = 1,
                SecurityStatus = "",
            }
            return new Position()
            {
                Ticker = "AAA",
                CompanyName = "Test Company",
                PositionId = 1,
                Trades = new List<Trade>()
                {

                },
                Securities
            }
        }
    }
}
