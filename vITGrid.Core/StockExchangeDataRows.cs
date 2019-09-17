using System.Collections.Generic;

namespace vITGrid.Core
{
    public class StockExchangeDataRows
    {
        public StockExchangeDataRows()
        {
            DataRows = new List<StockExchangeDataRow>();
        }

        public List<StockExchangeDataRow> DataRows { get; set; }
    }
}