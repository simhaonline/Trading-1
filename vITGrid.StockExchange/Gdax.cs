using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.StockExchange
{
    public class Gdax : StockExchangeBase, IStockExchanges
    {
        public Gdax()
        {
            BaseUri = "https://api.gdax.com/products";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers ticker = new Tickers(Common.StockExchange.Types.StockExchange.Gdax);

            try
            {
                foreach(string currency in currencies.Split(','))
                {
                    string uri = $"{BaseUri}/{currency}/ticker";

                    HttpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
                    HttpWebRequest.UserAgent = ".NET Framework Test Client";

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

                            ticker.PairTickers.Add(new PairTickerVolume(
                                currency.Replace("-", "/"),
                                Convert.ToDecimal(resultsJson["price"]),
                                Convert.ToDecimal(resultsJson["bid"]),
                                Convert.ToDecimal(resultsJson["ask"]),
                                Convert.ToDecimal(resultsJson["volume"]))
                            );
                        }
                    }

                    Thread.Sleep(150);
                }
            }
            catch(Exception)
            {
                // ignored
            }

            return ticker;
        }
    }
}