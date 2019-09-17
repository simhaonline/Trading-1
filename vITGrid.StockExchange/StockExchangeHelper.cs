using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vITGrid.StockExchange
{
    public class StockExchangeHelper
    {
        public async Task<string> GetSelectedPairs(Common.StockExchange.Types.StockExchange stockExchange, string pairs, List<string> selectedPairs)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string results = string.Empty;

            await Task.Run(() =>
            {
                if(selectedPairs.Count > 0)
                {
                    switch(stockExchange)
                    {
                        case Common.StockExchange.Types.StockExchange.Bitfinex:
                            foreach(string pair in pairs.Split(','))
                            {
                                foreach(string selectedPair in selectedPairs)
                                {
                                    if(pair.TrimStart('t').Equals(selectedPair.Replace("/", string.Empty)))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                        case Common.StockExchange.Types.StockExchange.Bitstamp:
                            foreach(string selectedPair in selectedPairs)
                            {
                                foreach(string pair in pairs.Split(','))
                                {
                                    if(pair.Equals(selectedPair.Replace("/", string.Empty).ToLower()))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                        case Common.StockExchange.Types.StockExchange.Gdax:
                            foreach(string selectedPair in selectedPairs)
                            {
                                foreach(string pair in pairs.Split(','))
                                {
                                    if(pair.Equals(selectedPair.Replace("/", "-")))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                        case Common.StockExchange.Types.StockExchange.Gemini:
                            foreach(string selectedPair in selectedPairs)
                            {
                                foreach(string pair in pairs.Split(','))
                                {
                                    if(pair.Equals(selectedPair.Replace("/", string.Empty).ToLower()))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                        case Common.StockExchange.Types.StockExchange.QuadrigaCx:
                            foreach(string selectedPair in selectedPairs)
                            {
                                foreach(string pair in pairs.Split(','))
                                {
                                    if(pair.Replace("_cad", "_usd").Equals(selectedPair.Replace("/", "_").ToLower()))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                        case Common.StockExchange.Types.StockExchange.Kraken:
                            foreach(string selectedPair in selectedPairs)
                            {
                                foreach(string pair in pairs.Split(','))
                                {
                                    if(pair.Replace("XBT", "BTC").Replace("DASH", "DSH").Equals(selectedPair.Replace("/", string.Empty)))
                                    {
                                        stringBuilder = stringBuilder.Append(pair).Append(",");
                                    }
                                }
                            }
                            results = stringBuilder.ToString().TrimEnd(',');
                            break;
                    }
                }
                else
                {
                    results = pairs;
                }
            });

            return results;
        }

        public async Task<List<string>> GetPairs(List<string> startCurrencies, List<string> endCurrencies)
        {
            List<string> pairs = new List<string>();

            await Task.Run(() =>
            {
                foreach(string startCurrency in startCurrencies)
                {
                    foreach(string endCurrency in endCurrencies)
                    {
                        pairs.Add($"{startCurrency}/{endCurrency}");
                    }
                }
            });
            
            return pairs;
        }
    }
}