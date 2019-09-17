namespace vITGrid.Common.StockExchange.Interfaces
{
    public interface IPairTickerVolumeHighLowPrice : IPairTickerVolume
    {
        decimal HighPrice { get; set; }
        decimal LowPrice { get; set; }
    }
}