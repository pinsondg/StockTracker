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
    }
}
