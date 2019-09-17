namespace vITGrid.Common.Common.Models
{
    public class CompositeStockExchangeObject
    {
        public CompositeStockExchangeObject(decimal exchangeRate, StockExchange.Types.StockExchange stockExchange, string selectedPairs)
        {
            ExchangeRate = exchangeRate;
            StockExchange = stockExchange;
            SelectedPairs = selectedPairs;
        }

        public decimal ExchangeRate { get; set; }
        public StockExchange.Types.StockExchange StockExchange { get; set; }
        public string SelectedPairs { get; set; }
    }
}