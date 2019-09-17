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
    public class Exmo : StockExchangeBase, IStockExchanges
    {
        public Exmo()
        {
            BaseUri = "https://api.exmo.com/v1";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            string uri = $"{BaseUri}/ticker{currencies}";

            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Exmo);

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
                            tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(
                                ticker.Key.Replace("_", "/"),
                                Convert.ToDecimal(ticker.Value["last_trade"]),
                                Convert.ToDecimal(ticker.Value["sell_price"]),
                                Convert.ToDecimal(ticker.Value["buy_price"]),
                                Convert.ToDecimal(ticker.Value["volume"]),
                                Convert.ToDecimal(ticker.Value["high"]),
                                Convert.ToDecimal(ticker.Value["low"]))
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