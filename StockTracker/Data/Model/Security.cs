using System;
namespace StockTracker.Data.Model
{
    public abstract class Security
    {
        public long SecurityId { get; set; }
        public string SecurityStatus { get; set; }
        public double Count { get; set; }
        public double AveragePrice { get; set; }

        public long PositionId { get; set; }
        public Position Position { get; set; }

        public Security()
        {
        }

        public abstract bool IsCurrentlyOpen();

        public abstract string GetDisplayName();

        public virtual bool CanHaveNegativeCount()
        {
            return false;
        }
    }
}
