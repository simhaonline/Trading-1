using System.Collections.Generic;

namespace vITGrid.Common.StockExchange.Interfaces
{
    public interface ITickers
    {
        Types.StockExchange StockExchange { get; set; }
        List<IPairTicker> PairTickers { get; set; }
    }
}