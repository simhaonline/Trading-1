using System.Collections.Generic;

namespace vITGrid.Common.StockExchange.Models
{
    public class GenericTickers
    {
        public GenericTickers()
        {
            Tickers = new List<GenericTicker>();
        }

        public List<GenericTicker> Tickers { get; set; }
    }
}