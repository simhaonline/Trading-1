using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.Common.StockExchange.Models
{
    public class PairTickerVolumeHighLowPrice : PairTickerVolume, IPairTickerVolumeHighLowPrice
    {
        public PairTickerVolumeHighLowPrice(string pair, decimal lastPrice, decimal bidPrice, decimal askPrice, decimal volume, decimal highPrice, decimal lowPrice) : base(pair, lastPrice, bidPrice, askPrice, volume)
        {
            HighPrice = highPrice;
            LowPrice = lowPrice;
        }

        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
    }
}