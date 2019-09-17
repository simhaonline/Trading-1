using System.Collections.Generic;
using System.Threading.Tasks;
using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.Core
{
    public class StockExchangeLastPriceRate
    {
        private static readonly int[] Intervals = { 5, 25, 50, 75, 150, 300, 600, 900, 1200, 1500, 1800, 2100, 2400, 2700, 3000, 3300, 3600, 3900, 4200, 4500, 4800, 5100, 5400, 5700, 6000, 6300, 6600, 6900, 7200 };

        public static readonly List<string> Headers = new List<string> { "Pair", "StockExchange", "LastPrice", "1 Minute", "5 Minutes", "10 Minutes", "15 Minutes", "30 Minutes", "1 Hour", "2 Hours", "3 Hours", "4 Hours", "5 Hours", "6 Hours", "7 Hours", "8 Hours", "9 Hours", "10 Hours", "11 Hours", "12 Hours", "13 Hours", "14 Hours", "15 Hours", "16 Hours", "17 Hours", "18 Hours", "19 Hours", "20 Hours", "21 Hours", "22 Hours", "23 Hours", "24 Hours" };

        public static readonly List<LastPriceRate> LastPriceRates = new List<LastPriceRate>(7200);

        private static void Initialize(List<ITickers> tickers, List<string> selectedPairs)
        {
            if(selectedPairs.Count > 0)
            {
                foreach(string selectedPair in selectedPairs)
                {
                    foreach(ITickers ticker in tickers)
                    {
                        foreach(IPairTicker pairTicker in ticker.PairTickers)
                        {
                            if(pairTicker.Pair.Equals(selectedPair))
                            {
                                LastPriceRates.Add(new LastPriceRate(ticker.StockExchange.ToString(), pairTicker.Pair,
                                    pairTicker.LastPrice));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(ITickers ticker in tickers)
                {
                    foreach(IPairTicker pairTicker in ticker.PairTickers)
                    {
                        LastPriceRates.Add(new LastPriceRate(ticker.StockExchange.ToString(), pairTicker.Pair,
                            pairTicker.LastPrice));
                    }
                }
            }
        }

        public static async Task UpdateLastPriceRates(List<ITickers> tickers, List<string> selectedPairs)
        {
            if(LastPriceRates.Count == 0)
            {
                Initialize(tickers, selectedPairs);
            }

            List<Task> tasks = new List<Task>();

            foreach(ITickers ticker in tickers)
            {
                foreach(IPairTicker pairTicker in ticker.PairTickers)
                {
                    foreach(LastPriceRate lastPriceRate in LastPriceRates)
                    {
                        if(lastPriceRate.StockExchange.Equals(ticker.StockExchange.ToString()) && pairTicker.Pair.Equals(lastPriceRate.Pair))
                        {
                            lastPriceRate.LastPrice = pairTicker.LastPrice;
                            tasks.Add(UpdateLastPriceRate(pairTicker.LastPrice, lastPriceRate));
                            break;
                        }
                    }
                }
            }
            
            await Task.WhenAll(tasks.ToArray());
        }

        private static async Task UpdateLastPriceRate(decimal lastPrice, LastPriceRate lastPriceRates)
        {
            lastPriceRates.LastPrices.Enqueue(lastPrice);

            if(lastPriceRates.LastPrices.Count > Intervals[Intervals.Length - 1])
            {
                lastPriceRates.LastPrices.Dequeue();
            }

            lastPriceRates.LastPriceRates = await GetPriceRates(lastPriceRates);
        }

        private static async Task<List<string>> GetPriceRates(LastPriceRate lastPriceRates)
        {
            List<string> results = new List<string>();

            await Task.Run(() =>
            {
                foreach(int interval in Intervals)
                {
                    if(lastPriceRates.LastPrices.Count >= interval)
                    {
                        decimal total = decimal.Zero;
                        decimal i = decimal.Zero;
                        foreach(decimal item in lastPriceRates.LastPrices)
                        {
                            if(i < interval)
                            {
                                total += item;
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        decimal average = total / interval;

                        if(average != decimal.Zero)
                        {
                            //decimal difference = (lastPriceRates.LastPrice - average) / average;
                            //results.Add(difference.ToString("P", CultureInfo.InvariantCulture));

                            //decimal difference = (lastPriceRates.LastPrice - average) / average;
                            results.Add(average.ToString("0.########"));
                        }
                        else
                        {
                            results.Add("0.0%");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            });

            return results;
        }
    }
}