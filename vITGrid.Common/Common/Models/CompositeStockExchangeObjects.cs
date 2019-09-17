using System.Collections.Generic;

namespace vITGrid.Common.Common.Models
{
    public class CompositeStockExchangeObjects
    {
        public CompositeStockExchangeObjects()
        {
            StockExchanges = new List<string>();
            SelectedPairs = new List<string>();
        }

        public CompositeStockExchangeObjects(decimal exchangeRate, List<string> stockExchanges, List<string> selectedPairs)
        {
            ExchangeRate = exchangeRate;
            StockExchanges = stockExchanges;
            SelectedPairs = selectedPairs;
        }

        public decimal ExchangeRate { get; set; }
        public List<string> StockExchanges { get; set; }
        public List<string> SelectedPairs { get; set; }
    }
}