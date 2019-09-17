namespace vITGrid.Core
{
    public class TradeValue
    {
        private readonly decimal _accountBalance;
        private readonly decimal _spendPercent;
        private readonly decimal _price;
        private readonly decimal _stopLossFromPercent;
        private readonly decimal _stopLossToPercent;
        private readonly decimal _profitPercent;

        public TradeValue(decimal accountBalance, decimal price, decimal spendPercent, decimal stopLossFromPercent, decimal stopLossToPercent, decimal profitPercent)
        {
            _accountBalance = accountBalance;
            _spendPercent = spendPercent;
            _price = price;
            _stopLossFromPercent = stopLossFromPercent;
            _stopLossToPercent = stopLossToPercent;
            _profitPercent = profitPercent;
        }

        public decimal SpendAmount => _accountBalance * (_spendPercent / 100);
        public decimal StopLossFrom => _price - (_price * (_stopLossFromPercent / 100));
        public decimal StopLossTo => _price - (_price * (_stopLossToPercent / 100));
        public decimal ProfitAmount => _price * (_profitPercent / 100);
        public decimal TotalProfit => _price + ProfitAmount;
    }
}