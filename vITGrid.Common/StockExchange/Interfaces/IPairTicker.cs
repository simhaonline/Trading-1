namespace vITGrid.Common.StockExchange.Interfaces
{
    public interface IPairTicker
    {
        string Pair { get; set; }
        decimal LastPrice { get; set; }
        decimal BidPrice { get; set; }
        decimal AskPrice { get; set; }
    }
}