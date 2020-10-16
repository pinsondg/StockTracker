using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StockTracker.Data.Model;

namespace StockTracker.Test.Service
{
    public class PositionServiceTest
    {
        protected PositionServiceTest(DbContextOptions<StockTrackerDBContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<StockTrackerDBContext> ContextOptions { get; }

        protected void Seed()
        {
            using (var context = new StockTrackerDBContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var Position1 = new Position
                {
                    CompanyName = "Test Company 1",
                    Ticker = "TEST",
                };

                context.Add(Position1);

                context.SaveChanges();
            }
        }
    }
}
