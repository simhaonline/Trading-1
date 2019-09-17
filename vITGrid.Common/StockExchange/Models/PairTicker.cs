using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.Common.StockExchange.Models
{
    public class PairTicker : IPairTicker
    {
        public PairTicker(string pair, decimal lastPrice, decimal bidPrice, decimal askPrice)
        {
            Pair = pair;
            LastPrice = lastPrice;
            BidPrice = bidPrice;
            AskPrice = askPrice;
        }

        public string Pair { get; set; }
        public decimal LastPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
    }
}