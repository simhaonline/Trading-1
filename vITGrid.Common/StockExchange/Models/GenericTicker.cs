namespace vITGrid.Common.StockExchange.Models
{
    public class GenericTicker
    {
        public GenericTicker(string id, string name, string symbol, string rank, string priceUsd, string priceBtc, string volume1DayUsd, string marketCapUsd, string availableSupply, string totalSupply, string maximumSupply, string percentChange1Hour, string percentChange1Day, string percentChange1Week, string lastUpdated)
        {
            Id = id;
            Name = name;
            Symbol = symbol;
            Rank = rank;
            PriceUsd = priceUsd;
            PriceBtc = priceBtc;
            Volume1DayUsd = volume1DayUsd;
            MarketCapUsd = marketCapUsd;
            AvailableSupply = availableSupply;
            TotalSupply = totalSupply;
            MaximumSupply = maximumSupply;
            PercentChange1Hour = percentChange1Hour;
            PercentChange1Day = percentChange1Day;
            PercentChange1Week = percentChange1Week;
            LastUpdated = lastUpdated;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Rank { get; set; }
        public string PriceUsd { get; set; }
        public string PriceBtc { get; set; }
        public string Volume1DayUsd { get; set; }
        public string MarketCapUsd { get; set; }
        public string AvailableSupply { get; set; }
        public string TotalSupply { get; set; }
        public string MaximumSupply { get; set; }
        public string PercentChange1Hour { get; set; }
        public string PercentChange1Day { get; set; }
        public string PercentChange1Week { get; set; }
        public string LastUpdated { get; set; }
    }
}