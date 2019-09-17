using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.Core
{
    public class StockExchangeGrids
    {
        public StockExchangeGrids()
        {
            
        }

        public async Task<StockExchangePairTable> GetCryptoMarketCapTable(GenericTickers tickers, List<string> selectedPairs)
        {
            StockExchangePairTable stockExchangePairTable = new StockExchangePairTable();

            await Task.Run(() =>
            {
                //stockExchangePairTable.HeaderNames.Add("Id");
                stockExchangePairTable.HeaderNames.Add("Rank");
                stockExchangePairTable.HeaderNames.Add("Name");
                stockExchangePairTable.HeaderNames.Add("Symbol");
                stockExchangePairTable.HeaderNames.Add("Price USD");
                stockExchangePairTable.HeaderNames.Add("Price BTC");
                stockExchangePairTable.HeaderNames.Add("Volume 24 Hours USD");
                stockExchangePairTable.HeaderNames.Add("Market Cap USD");
                stockExchangePairTable.HeaderNames.Add("Available Supply");
                stockExchangePairTable.HeaderNames.Add("Total Supply");
                stockExchangePairTable.HeaderNames.Add("Maximum Supply");
                stockExchangePairTable.HeaderNames.Add("Percent Change 1 Hour");
                stockExchangePairTable.HeaderNames.Add("Percent Change 1 Day");
                stockExchangePairTable.HeaderNames.Add("Percent Change 1 Week");
                stockExchangePairTable.HeaderNames.Add("Last Updated");

                if(selectedPairs.Count > 0)
                {
                    foreach(GenericTicker ticker in tickers.Tickers)
                    {
                        foreach(string selectedPair in selectedPairs)
                        {
                            if(ticker.Symbol.Equals(selectedPair.Split('/')[0]) || ticker.Symbol.Equals(selectedPair.Split('/')[1]))
                            {
                                StockExchangeDataRow stockExchangeDataRow = new StockExchangeDataRow();
                                //stockExchangeDataRow.DataRows.Add(ticker.Id);
                                stockExchangeDataRow.DataRows.Add(ticker.Rank);
                                stockExchangeDataRow.DataRows.Add(ticker.Name);
                                stockExchangeDataRow.DataRows.Add(ticker.Symbol.ToUpper());
                                stockExchangeDataRow.DataRows.Add(ticker.PriceUsd);
                                stockExchangeDataRow.DataRows.Add(ticker.PriceBtc);
                                stockExchangeDataRow.DataRows.Add(ticker.Volume1DayUsd);
                                stockExchangeDataRow.DataRows.Add(ticker.MarketCapUsd);
                                stockExchangeDataRow.DataRows.Add(ticker.AvailableSupply);
                                stockExchangeDataRow.DataRows.Add(ticker.TotalSupply);
                                stockExchangeDataRow.DataRows.Add(ticker.MaximumSupply);
                                stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Hour);
                                stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Day);
                                stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Week);
                                stockExchangeDataRow.DataRows.Add(ticker.LastUpdated);
                                stockExchangePairTable.StockExchangeDataRows.Add(stockExchangeDataRow);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach(GenericTicker ticker in tickers.Tickers)
                    {
                       StockExchangeDataRow stockExchangeDataRow = new StockExchangeDataRow();
                        //stockExchangeDataRow.DataRows.Add(ticker.Id);
                        stockExchangeDataRow.DataRows.Add(ticker.Rank);
                        stockExchangeDataRow.DataRows.Add(ticker.Name);
                        stockExchangeDataRow.DataRows.Add(ticker.Symbol.ToUpper());
                        stockExchangeDataRow.DataRows.Add(ticker.PriceUsd);
                        stockExchangeDataRow.DataRows.Add(ticker.PriceBtc);
                        stockExchangeDataRow.DataRows.Add(ticker.Volume1DayUsd);
                        stockExchangeDataRow.DataRows.Add(ticker.MarketCapUsd);
                        stockExchangeDataRow.DataRows.Add(ticker.AvailableSupply);
                        stockExchangeDataRow.DataRows.Add(ticker.TotalSupply);
                        stockExchangeDataRow.DataRows.Add(ticker.MaximumSupply);
                        stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Hour);
                        stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Day);
                        stockExchangeDataRow.DataRows.Add(ticker.PercentChange1Week);
                        stockExchangeDataRow.DataRows.Add(ticker.LastUpdated);
                        stockExchangePairTable.StockExchangeDataRows.Add(stockExchangeDataRow);
                    }
                }
            });
                
            return stockExchangePairTable;
        }

        public async Task<StockExchangePairTableDetails> GetStockExchangePairCompareTableDetails(List<ITickers> tickers, List<string> selectedPairs)
        {
            StockExchangePairTableDetails stockExchangePairTable = new StockExchangePairTableDetails();

            await Task.Run(() =>
            {
                stockExchangePairTable.HeaderNames.Add("PAIR");
                stockExchangePairTable.HeaderNames.Add("BUY");
                stockExchangePairTable.HeaderNames.Add("%");
                stockExchangePairTable.HeaderNames.Add("SELL");
                stockExchangePairTable.HeaderNames.Add(string.Empty);
                stockExchangePairTable.HeaderNames.Add("AMOUNT");
                stockExchangePairTable.HeaderNames.Add(string.Empty);
                stockExchangePairTable.HeaderNames.Add("PAIR");
                stockExchangePairTable.HeaderNames.Add("BUY");
                stockExchangePairTable.HeaderNames.Add("%");
                stockExchangePairTable.HeaderNames.Add("SELL");
                stockExchangePairTable.HeaderNames.Add(string.Empty);
                stockExchangePairTable.HeaderNames.Add("AMOUNT");

                if(selectedPairs.Count > 0)
                {
                    int l = 1;
                    foreach(ITickers ticker in tickers)
                    {
                        int i = l;
                        while(i < tickers.Count)
                        {
                            StockExchangeDataRows stockExchangeFilteredDataRows = GetStockExchangeFilteredDataRows(ticker, tickers[i], selectedPairs);
                            if(stockExchangeFilteredDataRows.DataRows.Count > 0)
                            {
                                stockExchangePairTable.StockExchangeDataRows.Add(stockExchangeFilteredDataRows);
                            }
                            i++;
                        }
                        l++;
                    }
                }
                else
                {
                    int l = 1;
                    foreach(ITickers ticker in tickers)
                    {
                        int i = l;
                        while(i < tickers.Count)
                        {
                            StockExchangeDataRows stockExchangeDataRows = GetStockExchangeDataRows(ticker, tickers[i]);
                            if(stockExchangeDataRows.DataRows.Count > 0)
                            {
                                stockExchangePairTable.StockExchangeDataRows.Add(stockExchangeDataRows);
                            }
                            i++;
                        }
                        l++;
                    }
                }
            });

            return stockExchangePairTable;
        }

        private StockExchangeDataRows GetStockExchangeDataRows(ITickers ticker1, ITickers ticker2)
        {
            StockExchangeDataRows stockExchangeDataRows = new StockExchangeDataRows();

            bool isFirstTime = true;
            
            foreach(IPairTicker pairTicker1 in ticker1.PairTickers)
            {
                foreach(IPairTicker pairTicker2 in ticker2.PairTickers)
                {
                    if(pairTicker1.Pair.Equals(pairTicker2.Pair))
                    {
                        StockExchangeDataRow stockExchangeDataRow;
                        if(isFirstTime)
                        {
                            isFirstTime = false;
                            stockExchangeDataRow = new StockExchangeDataRow();

                            stockExchangeDataRow.DataRows.Add("Pair");
                            stockExchangeDataRow.DataRows.Add($"Buy - {ticker1.StockExchange.ToString()}");
                            stockExchangeDataRow.DataRows.Add("%");
                            stockExchangeDataRow.DataRows.Add($"Sell - {ticker2.StockExchange.ToString()}");
                            stockExchangeDataRow.DataRows.Add(string.Empty);
                            stockExchangeDataRow.DataRows.Add("Amount");

                            stockExchangeDataRow.DataRows.Add(string.Empty);

                            stockExchangeDataRow.DataRows.Add("Pair");
                            stockExchangeDataRow.DataRows.Add($"Buy - {ticker2.StockExchange.ToString()}");
                            stockExchangeDataRow.DataRows.Add("%");
                            stockExchangeDataRow.DataRows.Add($"Sell - {ticker1.StockExchange.ToString()}");
                            stockExchangeDataRow.DataRows.Add(string.Empty);
                            stockExchangeDataRow.DataRows.Add("Amount");

                            stockExchangeDataRows.DataRows.Add(stockExchangeDataRow);
                        }

                        decimal priceDifferenceBuy = pairTicker2.BidPrice - pairTicker1.AskPrice;
                        decimal differenceRateBuy = pairTicker2.BidPrice != 0 ? priceDifferenceBuy / pairTicker2.BidPrice : 0m;
                        decimal priceDifferenceSell = pairTicker1.BidPrice - pairTicker2.AskPrice;
                        decimal differenceRateSell = pairTicker1.BidPrice != 0 ? priceDifferenceSell / pairTicker1.BidPrice : 0m;

                        stockExchangeDataRow = new StockExchangeDataRow();

                        stockExchangeDataRow.DataRows.Add(pairTicker1.Pair);
                        stockExchangeDataRow.DataRows.Add(pairTicker1.AskPrice.ToString("0.########"));
                        stockExchangeDataRow.DataRows.Add(differenceRateBuy.ToString("P", CultureInfo.InvariantCulture));
                        stockExchangeDataRow.DataRows.Add(pairTicker2.BidPrice.ToString("0.########"));
                        stockExchangeDataRow.DataRows.Add(string.Empty);
                        stockExchangeDataRow.DataRows.Add(priceDifferenceBuy.ToString("0.########"));

                        stockExchangeDataRow.DataRows.Add(string.Empty);

                        stockExchangeDataRow.DataRows.Add(pairTicker2.Pair);
                        stockExchangeDataRow.DataRows.Add(pairTicker2.AskPrice.ToString("0.########"));
                        stockExchangeDataRow.DataRows.Add(differenceRateSell.ToString("P", CultureInfo.InvariantCulture));
                        stockExchangeDataRow.DataRows.Add(pairTicker1.BidPrice.ToString("0.########"));
                        stockExchangeDataRow.DataRows.Add(string.Empty);
                        stockExchangeDataRow.DataRows.Add(priceDifferenceSell.ToString("0.########"));

                        stockExchangeDataRows.DataRows.Add(stockExchangeDataRow);
                    }
                }
            }

            return stockExchangeDataRows;
        }

        private StockExchangeDataRows GetStockExchangeFilteredDataRows(ITickers ticker1, ITickers ticker2, List<string> selectedPairs)
        {
           StockExchangeDataRows stockExchangeDataRows = new StockExchangeDataRows();

            bool isFirstTime = true;
            foreach(string selectedPair in selectedPairs)
            {
                foreach(IPairTicker pairTicker1 in ticker1.PairTickers)
                {
                    if(pairTicker1.Pair.Equals(selectedPair))
                    {
                        foreach(IPairTicker pairTicker2 in ticker2.PairTickers)
                        {
                            if(pairTicker1.Pair.Equals(pairTicker2.Pair))
                            {
                                StockExchangeDataRow stockExchangeDataRow;
                                if(isFirstTime)
                                {
                                    isFirstTime = false;
                                    stockExchangeDataRow = new StockExchangeDataRow();

                                    stockExchangeDataRow.DataRows.Add("Pair");
                                    stockExchangeDataRow.DataRows.Add($"Buy - {ticker1.StockExchange.ToString()}");
                                    stockExchangeDataRow.DataRows.Add("%");
                                    stockExchangeDataRow.DataRows.Add($"Sell - {ticker2.StockExchange.ToString()}");
                                    stockExchangeDataRow.DataRows.Add(string.Empty);
                                    stockExchangeDataRow.DataRows.Add("Amount");

                                    stockExchangeDataRow.DataRows.Add(string.Empty);

                                    stockExchangeDataRow.DataRows.Add("Pair");
                                    stockExchangeDataRow.DataRows.Add($"Buy - {ticker2.StockExchange.ToString()}");
                                    stockExchangeDataRow.DataRows.Add("%");
                                    stockExchangeDataRow.DataRows.Add($"Sell - {ticker1.StockExchange.ToString()}");
                                    stockExchangeDataRow.DataRows.Add(string.Empty);
                                    stockExchangeDataRow.DataRows.Add("Amount");

                                    stockExchangeDataRows.DataRows.Add(stockExchangeDataRow);
                                }

                                decimal priceDifferenceBuy = pairTicker2.BidPrice - pairTicker1.AskPrice;
                                decimal differenceRateBuy = pairTicker2.BidPrice != 0 ? priceDifferenceBuy / pairTicker2.BidPrice : 0m;
                                decimal priceDifferenceSell = pairTicker1.BidPrice - pairTicker2.AskPrice;
                                decimal differenceRateSell = pairTicker1.BidPrice != 0 ? priceDifferenceSell / pairTicker1.BidPrice : 0m;

                                stockExchangeDataRow = new StockExchangeDataRow();

                                stockExchangeDataRow.DataRows.Add(pairTicker1.Pair);
                                stockExchangeDataRow.DataRows.Add(pairTicker1.AskPrice.ToString("0.########"));
                                stockExchangeDataRow.DataRows.Add(differenceRateBuy.ToString("P", CultureInfo.InvariantCulture));
                                stockExchangeDataRow.DataRows.Add(pairTicker2.BidPrice.ToString("0.########"));
                                stockExchangeDataRow.DataRows.Add(string.Empty);
                                stockExchangeDataRow.DataRows.Add(priceDifferenceBuy.ToString("0.########"));

                                stockExchangeDataRow.DataRows.Add(string.Empty);

                                stockExchangeDataRow.DataRows.Add(pairTicker2.Pair);
                                stockExchangeDataRow.DataRows.Add(pairTicker2.AskPrice.ToString("0.########"));
                                stockExchangeDataRow.DataRows.Add(differenceRateSell.ToString("P", CultureInfo.InvariantCulture));
                                stockExchangeDataRow.DataRows.Add(pairTicker1.BidPrice.ToString("0.########"));
                                stockExchangeDataRow.DataRows.Add(string.Empty);
                                stockExchangeDataRow.DataRows.Add(priceDifferenceSell.ToString("0.########"));

                                stockExchangeDataRows.DataRows.Add(stockExchangeDataRow);
                            }
                        }
                    }
                }
            }
            
            return stockExchangeDataRows;
        }
    }
}