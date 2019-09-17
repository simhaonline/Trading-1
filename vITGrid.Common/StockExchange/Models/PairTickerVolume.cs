using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.Common.StockExchange.Models
{
    public class PairTickerVolume : PairTicker, IPairTickerVolume
    {
        public PairTickerVolume(string pair, decimal lastPrice, decimal bidPrice, decimal askPrice, decimal volume) : base(pair, lastPrice, bidPrice, askPrice)
        {
            Volume = volume;
        }

        public decimal Volume { get; set; }
    }
}