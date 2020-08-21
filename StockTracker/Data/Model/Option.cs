using System;
namespace StockTracker.Data.Model
{
    public class Option : Security
    {
        public double Strike { get; set; }
        public DateTime ExpirationDate { get; set; }
        public OptionType Type { get; set; }

        public Option()
        {
        }

        public enum OptionType
        {
            CALL, PUT, COVERED_CALL, CASH_SECURED_PUT
        }

        public override bool IsCurrentlyOpen()
        {
            throw new NotImplementedException();
        }

        public override string GetDisplayName()
        {
            var type = "";
            switch (Type)
            {
                case OptionType.CALL:
                    type = "C";
                    break;
                case OptionType.PUT:
                    type = "P";
                    break;
                case OptionType.COVERED_CALL:
                    type = "CC";
                    break;
                case OptionType.CASH_SECURED_PUT:
                    type = "CSP";
                    break;
            }
            return String.Format("{0} - {1:C2} - {2}", type, Strike, ExpirationDate.ToShortDateString());
        }
    }
}
