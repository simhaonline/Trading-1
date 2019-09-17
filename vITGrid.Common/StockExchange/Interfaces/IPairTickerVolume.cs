namespace vITGrid.Common.StockExchange.Interfaces
{
    public interface IPairTickerVolume : IPairTicker
    {
        decimal Volume { get; set; }
    }
}