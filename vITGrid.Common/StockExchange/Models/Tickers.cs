using System.Collections.Generic;
using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.Common.StockExchange.Models
{
    public class Tickers : ITickers
    {
        public Tickers(Types.StockExchange stockExchange)
        {
            StockExchange = stockExchange;
            PairTickers = new List<IPairTicker>();
        }

        public Types.StockExchange StockExchange { get; set; }
        public List<IPairTicker> PairTickers { get; set; }
    }
}