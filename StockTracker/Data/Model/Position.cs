using System;
using System.Collections.Generic;

namespace StockTracker.Data.Model
{
    public class Position
    {
        public long PositionId { get; set; }
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public List<Trade> Trades { get; set; }
        public List<Security> Securities { get; set; }

        public Position()
        {
        }

        public void GetCurrentHoldings()
        {

        }

        public double GetRealizedGainsDividends()
        {
            //List<Trade> buys = Trades.FindAll(trade => trade.TradeAction == TradeAction.BUY && trade.Security is Option);
            //List<Trade> sells = Trades.FindAll(trade => trade.TradeAction == TradeAction.SELL && trade.Security is Option);

            throw new NotImplementedException();
        }
    }
}
