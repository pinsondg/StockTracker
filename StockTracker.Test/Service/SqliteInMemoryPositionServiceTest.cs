using System;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using StockTracker.Data.Model;
using StockTracker.Data.Service;

namespace StockTracker.Test.Service
{
    [TestFixture]
    public class SqliteInMemoryPositionServiceTest : PositionServiceTest, IDisposable
    {
        private readonly DbConnection _connection;

        public SqliteInMemoryPositionServiceTest()
            : base(
            new DbContextOptionsBuilder<StockTrackerDBContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
    
            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();


        [Test]
        public void TestAddPosition_PositionSaved()
        {
            using (var context = new StockTrackerDBContext(ContextOptions))
            {
                var positionService = new PositionService(context);
                Position position = new Position
                {
                    CompanyName = "Test Position 2",
                    Ticker = "TEST2"
                };
                position = positionService.AddPosition(position);
                Assert.AreEqual("TEST2", position.Ticker);
            }
            using (var contex = new StockTrackerDBContext(ContextOptions))
            {
                var position = contex.Set<Position>().Single(e => e.Ticker == "TEST2");
                Assert.AreEqual("TEST2", position.Ticker);
                Assert.AreEqual("Test Position 2", position.CompanyName);
            }
        }

        [Test]
        public void TestAddTradeToPosition_TradeSaved()
        {
            using (var context = new StockTrackerDBContext(ContextOptions))
            {
                var positionService = new PositionService(context);
                var position = context.Positions.Single(e => e.Ticker == "TEST");

                Trade trade = new Trade
                {
                    TradeAction = TradeAction.BUY,
                    TradeContractCount = 100,
                    TradePricePerContract = 1,
                    TradeDate = DateTime.Now,
                    TradeOrder = 0,
                    Security = new Stock()                
                };

                Trade trade2 = new Trade
                {
                    TradeAction = TradeAction.SELL,
                    TradeContractCount = 100,
                    TradePricePerContract = .20,
                    TradeDate = DateTime.Now,
                    TradeOrder = 0,
                    Security = new Option
                    {
                        ExpirationDate = DateTime.Now.AddDays(20),
                        Strike = 2.30,
                        Type = Option.OptionType.COVERED_CALL
                    }
                };

                Trade trade3 = new Trade
                {
                    TradeAction = TradeAction.BUY,
                    TradeContractCount = 100,
                    TradePricePerContract = .12,
                    TradeDate = DateTime.Now.AddDays(1),
                    TradeOrder = 0,
                    Security = new Option
                    {
                        ExpirationDate = DateTime.Now.AddDays(20),
                        Strike = 2.30,
                        Type = Option.OptionType.COVERED_CALL
                    }
                };

                positionService.MakeTradeOnPosition(position.PositionId, trade);
                positionService.MakeTradeOnPosition(position.PositionId, trade2);

                Assert.IsNotNull(position);
                Assert.IsNotNull(position.Trades);
                Assert.IsNotNull(position.Securities);
                Assert.AreEqual(2, position.Trades.Count);
                Assert.AreEqual(2, position.Securities.Count);
                Assert.AreEqual(100, position.Trades[0].TradeContractCount);
            }
        }

        [Test]
        public void TestRemovePosition_PostitionRemoved()
        {
            using (var context = new StockTrackerDBContext(ContextOptions))
            {
                var positionService = new PositionService(context);
                var position = context.Positions.Single(e => e.Ticker == "TEST");

                Assert.AreEqual(2, positionService.GetAllPositions().Count());

                positionService.RemovePostion(position);

                Assert.AreEqual(1, positionService.GetAllPositions().Count());
            }
        }
    }
}
