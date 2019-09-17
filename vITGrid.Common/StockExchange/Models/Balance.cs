namespace vITGrid.Common.StockExchange.Models
{
    public class Balance
    {
        public Balance(string account, string currency, decimal availableBalance, decimal amountBalance)
        {
            Account = account;
            Currency = currency;
            AvailableBalance = availableBalance;
            AmountBalance = amountBalance;
        }

        public string Account { get; set; }
        public string Currency { get; set; }
        public decimal AmountBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}