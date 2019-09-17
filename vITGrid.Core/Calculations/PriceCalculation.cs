namespace vITGrid.Core.Calculations
{
    public class PriceCalculation
    {
        private readonly decimal _purchasePrice;
        private readonly decimal _salePrice;
        private readonly decimal _numberOfCoins;
        private readonly decimal _transactionFee;

        public PriceCalculation(decimal numberOfCoins, decimal purchasePrice, decimal salePrice, decimal transactionFee)
        {
            _numberOfCoins = numberOfCoins;
            _purchasePrice = purchasePrice;
            _salePrice = salePrice;
            _transactionFee = transactionFee;
        }

        public decimal GetGrossProfit()
        {
            return (_numberOfCoins * _salePrice) - (_numberOfCoins * _purchasePrice);
        }

        public decimal GetPriceDifference()
        {
            return _salePrice - _purchasePrice;
        }

        public decimal GetSaleRate()
        {
            return GetPriceDifference() / _purchasePrice;
        }

        public decimal GetSaleRateAmount(decimal percent)
        {
            return (_purchasePrice / 100) * percent;
        }

        public decimal GetSaleRateTotalAmount(decimal percent)
        {
            return GetSaleRateAmount(percent) + _purchasePrice;
        }

        public decimal GetNetProfit()
        {
            return GetGrossProfit() - _transactionFee;
        }
    }
}