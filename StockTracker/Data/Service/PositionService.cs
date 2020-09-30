using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StockTracker.Data.Model;

namespace StockTracker.Data.Service
{
    public class PositionService : IPositionService
    {
        private readonly StockTrackerDBContext _dbContext;
        private readonly IStockDataService _stockDataService;

        public PositionService(StockTrackerDBContext dbContext, IStockDataService stockDataService)
        {
            _dbContext = dbContext;
            _stockDataService = stockDataService;
        }

        public Position AddPosition(Position position)
        {
            _dbContext.Positions.Add(position);
            _dbContext.SaveChanges();
            return position;
        }

        public Task<List<Position>> GetAllPositionsAsync()
        {
            return _dbContext.Positions
               .Include(position => position.Securities)
                .Include(position => position.Trades)
                    .ThenInclude(trade => trade.Security)
                .ToListAsync();
        }

        public List<Position> GetAllPositions()
        {
            return _dbContext.Positions
                .Include(position => position.Securities)
                .Include(position => position.Trades)
                    .ThenInclude(trade => trade.Security)
                .ToList();
        }

        public double GetPositionCurrentOpenPL(Position position)
        {
            Security openStock = position.GetCurrentHoldings().Find(holding => holding is Stock);
            if (openStock != null)
            {
                var currentStockPL = _stockDataService.GetCurrentStockData(position.Ticker).tngoLast.GetValueOrDefault(0) * openStock.Count - openStock.AveragePrice * openStock.Count;
                return currentStockPL + position.GetTotalRelizedGains();
            }
            return 0;
        }      

        public Position MakeTradeOnPosition(long positonId, Trade trade)
        {
         
            Position position = _dbContext.Positions
                .Include(position => position.Securities)
                .Include(position => position.Trades)
                    .ThenInclude(trade => trade.Security)
                .ToList()
                .Find(x => x.PositionId == positonId);
            trade.Security.Position = position;
            if (position.Trades == null)
            {
                position.Trades = new List<Trade>();
            }
            if (position.Securities == null)
            {
                position.Securities = new List<Security>();
            }
            var tradeCount = position.Trades.Count;
            trade.TradeOrder = tradeCount;

            GetExistingSecurityIfAvailible(position, trade);
            position.Trades.Add(trade);           
            _dbContext.SaveChanges();
            return position;                     
        }

        public void RemovePostion(Position position)
        {
            var positionToDelete = _dbContext.Positions.Find(position.PositionId);
            _dbContext.Positions.Remove(positionToDelete);
            _dbContext.SaveChanges();
        }

        private void GetExistingSecurityIfAvailible(Position position, Trade trade)
        {
            Security found = position.Securities.Find(security => trade.Security.Equals(security));
            if (found != null)
            {
                if (trade.TradeAction == TradeAction.BUY)
                {
                    found.AveragePrice = ((trade.TradeContractCount * trade.TradePricePerContract) + (found.Count * found.AveragePrice)) / (trade.TradeContractCount + found.Count);
                    found.Count += trade.TradeContractCount;
                } else if (trade.Security.CanHaveNegativeCount() || (!trade.Security.CanHaveNegativeCount() && trade.TradeContractCount <= found.Count))
                {
                    found.Count -= trade.TradeContractCount;
                } else
                {
                    throw new Exception("Cannot have negative shares of this security.");
                }
                trade.Security = found;               
            } else
            {
                if (trade.TradeAction == TradeAction.BUY)
                {
                    trade.Security.Count = trade.TradeContractCount;
                } else if (trade.Security.CanHaveNegativeCount())
                {
                    trade.Security.Count = -trade.TradeContractCount;
                } else
                {
                    throw new Exception("Cannot have negative shares of this security.");
                }
                trade.Security.AveragePrice = trade.TradePricePerContract;
                position.Securities.Add(trade.Security);
            }
        }
    }
}
