using System;
using System.Runtime.Serialization;

namespace StockTracker.Data.Adapter.Tiingo
{
    [DataContract]
    public class TiingoStockSearchResponse
    {
        [DataMember]
        public string ticker { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string assetType { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string permaTicker { get; set; }
        [DataMember]
        public string openFIGI { get; set; }

        public TiingoStockSearchResponse()
        {
        }
    }
}
