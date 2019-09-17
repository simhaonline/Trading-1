using System.Collections.Generic;

namespace vITGrid.Core
{
    public class StockExchangeDataRow
    {
        public StockExchangeDataRow()
        {
            DataRows = new List<string>();
        }

        public List<string> DataRows { get; set; }
    }
}