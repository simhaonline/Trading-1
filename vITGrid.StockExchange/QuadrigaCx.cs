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
    public class QuadrigaCx : StockExchangeBase, IStockExchanges
    {
        public QuadrigaCx()
        {
            BaseUri = "https://api.quadrigacx.com/v2";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.QuadrigaCx);

            try
            {
                foreach(string currency in currencies.Split(','))
                {
                    string uri = $"{BaseUri}/ticker?book={currency}";

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

                            string pair = currency.Replace("_", "/").ToUpper();
                            decimal lastPrice = Convert.ToDecimal(resultsJson["last"]);
                            decimal bidPrice = Convert.ToDecimal(resultsJson["bid"]);
                            decimal askPrice = Convert.ToDecimal(resultsJson["ask"]);
                            decimal highPrice = Convert.ToDecimal(resultsJson["high"]);
                            decimal lowPrice = Convert.ToDecimal(resultsJson["low"]);
                            decimal volume = Convert.ToDecimal(resultsJson["volume"]);

                            if(!pair.Equals("BTC/CAD"))
                            {
                                if(pair.Split('/')[1].Equals("CAD"))
                                {
                                    pair = pair.Replace("CAD", "USD");
                                    lastPrice *= exchangeRate;
                                    bidPrice *= exchangeRate;
                                    askPrice *= exchangeRate;
                                    highPrice *= exchangeRate;
                                    lowPrice *= exchangeRate;
                                }

                                tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(pair, lastPrice, bidPrice, askPrice, volume, highPrice, lowPrice));
                            }
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