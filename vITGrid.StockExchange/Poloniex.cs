using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.StockExchange
{
    public class Poloniex : StockExchangeBase, IStockExchanges
    {
        public Poloniex()
        {
            BaseUri = "https://poloniex.com";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            string uri = $"{BaseUri}/public?command=returnTicker{currencies}";

            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Poloniex);

            try
            {
                HttpWebRequest = WebRequest.Create(uri) as HttpWebRequest;

                if(HttpWebRequest != null)
                {
                    HttpWebRequest.Method = "GET";
                    HttpWebRequest.Accept = "application/json";

                    using(HttpWebResponse = await HttpWebRequest.GetResponseAsync() as HttpWebResponse)
                    {
                        if(HttpWebResponse != null)
                        {
                            StreamReader = new StreamReader(HttpWebResponse.GetResponseStream() ?? throw new InvalidOperationException());
                        }

                        JObject resultsJson = JsonConvert.DeserializeObject<JObject>(StreamReader.ReadToEnd());

                        foreach(KeyValuePair<string, JToken> ticker in resultsJson)
                        {
                            tickers.PairTickers.Add(new PairTickerVolume(
                                ticker.Key.Replace("_", "/"),
                                Convert.ToDecimal(ticker.Value["last"]),
                                Convert.ToDecimal(ticker.Value["highestBid"]),
                                Convert.ToDecimal(ticker.Value["lowestAsk"]),
                                Convert.ToDecimal(ticker.Value["baseVolume"]))
                            );
                        }
                    }
                }
            }
            catch(Exception)
            {
                // ignored
            }

            return tickers;
        }
    }
}