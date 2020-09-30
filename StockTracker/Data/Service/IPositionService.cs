using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockTracker.Data.Model;

namespace StockTracker.Data.Service
{
    public interface IPositionService
    {

        public Task<List<Position>> GetAllPositionsAsync();

        public Position AddPosition(Position position);

        public Position MakeTradeOnPosition(long positionId, Trade trade);

        public void RemovePostion(Position position);

        public List<Position> GetAllPositions();

        public double GetPositionCurrentOpenPL(Position position);
    }
}
