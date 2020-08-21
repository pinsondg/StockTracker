using System;
namespace StockTracker.Data.Model
{
    public class Dividend : Security
    {
        public Dividend()
        {
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
