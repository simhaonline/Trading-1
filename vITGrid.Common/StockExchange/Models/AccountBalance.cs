using System.Collections.Generic;

namespace vITGrid.Common.StockExchange.Models
{
    public class AccountBalance
    {
        public AccountBalance(string stockExchange)
        {
            StockExchange = stockExchange;
            Balances = new List<Balance>();
        }

        public string StockExchange { get; set; }
        public List<Balance> Balances { get; set; }
    }
}