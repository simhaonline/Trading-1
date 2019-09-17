using Microsoft.VisualStudio.TestTools.UnitTesting;
using vITGrid.Core.Calculations;

namespace vITGrid.Core.Tests
{
    [TestClass]
    public class UnitTestPriceCalculations
    {
        private PriceCalculation _priceCalculation;

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestMethodGetGrossProfit()
        {
            _priceCalculation = new PriceCalculation(5m, 0.01020304m, 0.01020404m, 0);
            Assert.AreEqual(0.00000500m, _priceCalculation.GetGrossProfit());
        }

        [TestMethod]
        public void TestMethodGetNetProfit()
        {
            _priceCalculation = new PriceCalculation(5m, 0.01020304m, 0.01020404m, 0.002m);
            Assert.AreEqual(-0.00199500m, _priceCalculation.GetNetProfit());
        }

        [TestMethod]
        public void TestMethodGetPriceDifferencePositive()
        {
            _priceCalculation = new PriceCalculation(1m, 0.01020304m, 0.01020404m, 0);
            Assert.AreEqual(0.00000100m, _priceCalculation.GetPriceDifference());
        }

        [TestMethod]
        public void TestMethodGetPriceDifferenceNegative()
        {
            _priceCalculation = new PriceCalculation(1, 0.01020404m, 0.01020304m, 0);
            Assert.AreEqual(-0.00000100m, _priceCalculation.GetPriceDifference());
        }

        [TestMethod]
        public void TestMethodGetSaleRate()
        {
            _priceCalculation = new PriceCalculation(1, 0.01020304m, 0.01020404m, 0);
            Assert.AreEqual(0.0000980100048612962411202936m, _priceCalculation.GetSaleRate());

            _priceCalculation = new PriceCalculation(1, 0.01020304m, 0.01060404m, 0);
            Assert.AreEqual(0.0393020119493797926892377174m, _priceCalculation.GetSaleRate());
        }

        [TestMethod]
        public void TestMethodGetSaleRateAmount()
        {
            _priceCalculation = new PriceCalculation(1, 0.01021004m, 0.01022304m, 0);
            Assert.AreEqual(0.000515607020m, _priceCalculation.GetSaleRateAmount(5.05m));
        }

        [TestMethod]
        public void TestMethodGetSaleRateTotalAmount()
        {
            _priceCalculation = new PriceCalculation(1, 0.01021004m, 0.01022304m, 0);
            Assert.AreEqual(0.010725647020m, _priceCalculation.GetSaleRateTotalAmount(5.05m));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _priceCalculation = null;
        }
    }
}