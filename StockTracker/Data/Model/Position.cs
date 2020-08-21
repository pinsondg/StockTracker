using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracker.Data.Model
{
    public class Position
    {
        public long PositionId { get; set; }

        [Required]
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public List<Trade> Trades { get; set; }
        public List<Security> Securities { get; set; }

        public Position()
        {
            Trades = new List<Trade>();
            Securities = new List<Security>();
        }

        public void GetCurrentHoldings()
        {

        }

        public double GetRealizedGainsStock()
        {
            List<Trade> stockTrades = Trades.FindAll(trade => trade.Security is Stock);

            Stack<Trade> buyStack = new Stack<Trade>();
            double relizedGains = 0;
            foreach (var trade in stockTrades)
            {
                if (trade.TradeAction == TradeAction.BUY)
                {
                    buyStack.Push(trade);
                }
                else
                {
                    Trade averege = AverageBuyTrades(buyStack);
                    relizedGains += (trade.TradePricePerContract - averege.TradePricePerContract) * trade.TradeContractCount;
                    averege.TradeContractCount -= trade.TradeContractCount;
                    buyStack = new Stack<Trade>();
                    if (averege.TradeContractCount > 0)
                    {
                        buyStack.Push(averege);
                    }
                }
            }
            return relizedGains;
        }

        public double GetRealizedGainsDividends()
        {
            List<Trade> dividends = Trades.FindAll(trade => trade.Security is Dividend);

            return dividends.Sum(dividend => dividend.TradeContractCount * dividend.TradePricePerContract);
        }

        public double GetRealizedGainsOptionPremium()
        {
            List<Trade> buys = Trades.FindAll(trade => trade.TradeAction == TradeAction.BUY && trade.Security is Option);
            List<Trade> sells = Trades.FindAll(trade => trade.TradeAction == TradeAction.SELL && trade.Security is Option);

            double totalSellCost = sells.Sum(sell => sell.TradeContractCount * sell.TradePricePerContract);
            double totalBuyCost = buys.Sum(buy => buy.TradeContractCount * buy.TradePricePerContract);

            return totalSellCost - totalBuyCost;
        }

        public double GetTotalRelizedGains()
        {
            return GetRealizedGainsDividends() + GetRealizedGainsOptionPremium() + GetRealizedGainsStock();
        }

        private Trade AverageBuyTrades(Stack<Trade> buys)
        {
            var totalShares = 0.0;
            var totalCost = 0.0;
            foreach (Trade buy in buys)
            {
                totalShares += buy.TradeContractCount;
                totalCost += buy.TradeContractCount * buy.TradePricePerContract;
            }
            return new Trade()
            {
                Security = buys.First().Security,
                TradeContractCount = totalShares,
                TradePricePerContract = totalCost / totalShares
            };
        }

    }
}
