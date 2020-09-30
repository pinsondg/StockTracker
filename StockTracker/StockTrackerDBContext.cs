using System;
using Microsoft.EntityFrameworkCore;
using StockTracker.Data.Model;

namespace StockTracker
{
    public class StockTrackerDBContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<Trade> Trades { get; set; }

        public StockTrackerDBContext(DbContextOptions<StockTrackerDBContext> options)
            :base(options)
        {
        }
    }
}
