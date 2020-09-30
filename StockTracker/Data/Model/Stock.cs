using System;
namespace StockTracker.Data.Model
{
    public class Stock : Security
    {
        public Stock()
        {
        }

        public override string GetDisplayName()
        {
            return "Stock";
        }

        public override bool IsCurrentlyOpen()
        {
            return Count > 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Stock && ((Stock)obj).PositionId == PositionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SecurityId);
        }
    }
}
