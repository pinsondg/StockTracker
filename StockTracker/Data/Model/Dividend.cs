using System;
namespace StockTracker.Data.Model
{
    public class Dividend : Security
    {
        public Dividend()
        {
            Count = 1;
        }

        public override string GetDisplayName()
        {
            return "Div";
        }

        public override bool IsCurrentlyOpen()
        {
            return false;
        }
    }
}
