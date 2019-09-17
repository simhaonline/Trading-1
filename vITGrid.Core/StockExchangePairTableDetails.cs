using System.Collections.Generic;

namespace vITGrid.Core
{
    public class StockExchangePairTableDetails
    {
        public StockExchangePairTableDetails()
        {
            HeaderNames = new List<string>();
            StockExchangeDataRows = new List<StockExchangeDataRows>();
        }

        public List<string> HeaderNames { get; set; }
        public List<StockExchangeDataRows> StockExchangeDataRows { get; set; }
    }
}