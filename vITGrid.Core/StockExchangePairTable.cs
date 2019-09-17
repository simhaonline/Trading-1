using System.Collections.Generic;

namespace vITGrid.Core
{
    public class StockExchangePairTable
    {
        public StockExchangePairTable()
        {
            HeaderNames = new List<string>();
            StockExchangeDataRows = new List<StockExchangeDataRow>();
        }

        public List<string> HeaderNames { get; set; }
        public List<StockExchangeDataRow> StockExchangeDataRows { get; set; }
    }
}