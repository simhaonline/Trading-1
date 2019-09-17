using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.StockExchange
{
    public class Kraken : StockExchangeBase, IStockExchanges
    {
        public Kraken()
        {
            BaseUri = "https://api.kraken.com/0";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            string uri = $"{BaseUri}/public/Ticker?pair={currencies}";

            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Kraken);

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

                        foreach(JToken ticker in resultsJson["result"])
                        {

                            string pair = ((JProperty)ticker).Name;
                            decimal lastPrice = Convert.ToDecimal(((JProperty)ticker).Value["c"][0]);
                            decimal bidPrice = Convert.ToDecimal(((JProperty)ticker).Value["b"][0]);
                            decimal askPrice = Convert.ToDecimal(((JProperty)ticker).Value["a"][0]);
                            decimal volume = Convert.ToDecimal(((JProperty)ticker).Value["v"][0]);
                            decimal highPrice = Convert.ToDecimal(((JProperty)ticker).Value["h"][0]);
                            decimal lowPrice = Convert.ToDecimal(((JProperty)ticker).Value["l"][0]);

                            pair = pair.Replace("XBT", "BTC").Replace("DASH", "DSH");

                            if(pair.StartsWith("X") && pair.Length > 6)
                            {
                                pair = pair.Substring(1);
                            }

                            if(pair.Length > 6)
                            {
                                if(pair.Substring(3, 1).Equals("Z") || pair.Substring(3, 1).Equals("X"))
                                {
                                    pair = pair.Remove(3, 1);
                                    pair = pair.Insert(3, "/");
                                }
                                else
                                {
                                    pair = pair.Remove(4, 1);
                                    pair = pair.Insert(4, "/");
                                }
                            }
                            else
                            {
                                pair = pair.Insert(3, "/");
                            }

                            tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(pair, lastPrice, bidPrice, askPrice, volume, highPrice, lowPrice));
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