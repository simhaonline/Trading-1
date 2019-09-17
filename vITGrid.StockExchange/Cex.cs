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
    public class Cex : StockExchangeBase, IStockExchanges
    {
        public Cex()
        {
            BaseUri = "https://cex.io/api";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Cex);

            try
            {
                string uri = $"{BaseUri}/tickers/{currencies}";

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

                        foreach(JToken ticker in resultsJson["data"])
                        {
                            string pair = ticker["pair"].ToString().Replace(":", "/");

                            tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(
                                pair, 
                                Convert.ToDecimal(ticker["last"]),
                                Convert.ToDecimal(ticker["bid"]),
                                Convert.ToDecimal(ticker["ask"]),
                                Convert.ToDecimal(ticker["volume"]),
                                Convert.ToDecimal(ticker["high"]),
                                Convert.ToDecimal(ticker["low"]))
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