using System.Collections.Generic;

namespace vITGrid.Core
{
    public class LastPriceRate
    {
        public LastPriceRate(string stockExchange, string pair, decimal lastPrice)
        {
            StockExchange = stockExchange;
            Pair = pair;
            LastPrice = lastPrice;
            LastPrices = new Queue<decimal>();
            LastPriceRates = new List<string>();
        }

        public string StockExchange { get; set; }
        public string Pair { get; set; }
        public decimal LastPrice { get; set; }
        public Queue<decimal> LastPrices { get; set; }
        public List<string> LastPriceRates { get; set; }
    }
}