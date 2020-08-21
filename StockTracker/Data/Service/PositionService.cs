using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockTracker.Data.Model;

namespace StockTracker.Data.Service
{
    public class PositionService : IPositionService
    {
        private readonly StockTrackerDBContext _dbContext;

        public PositionService(StockTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Position AddPosition(Position position)
        {
            _dbContext.Positions.Add(position);
            _dbContext.SaveChanges();
            return position;
        }

        public Task<List<Position>> GetAllPositionsAsync()
        {
            return _dbContext.Positions.ToListAsync();
        }

        public double GetCurrentStockPriceForPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public Position MakeTradeOnPosition(Position positon, Trade trade)
        {
            throw new NotImplementedException();
        }

        public void RemovePostion(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
