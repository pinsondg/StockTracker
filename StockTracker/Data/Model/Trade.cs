using System;
namespace StockTracker.Data.Model
{
    public class Trade
    {
        public long TradeId { get; set; }
        public long TradeOrder { get; set; }
        public DateTime TradeDate { get; set; }
        public TradeAction TradeAction { get; set; }
        public double TradeContractCount { get; set; }
        public double TradePricePerContract { get; set; }
        public Security Security { get; set; }

        public long PositionId { get; set; }
        public Position Position { get; set; }

        public Trade()
        {
        }

    }

    public enum TradeAction
    {
        BUY, SELL
    }
}
