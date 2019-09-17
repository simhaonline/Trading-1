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
    public class Bittrex : StockExchangeBase, IStockExchanges
    {
        public Bittrex()
        {
            BaseUri = "https://bittrex.com/api/v1.1";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Bittrex);

            try
            {
                string uri = $"{BaseUri}/public/getMarketSummaries{currencies}";

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
                            string pair = ticker["MarketName"].ToString().Replace("-", "/");

                            tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(
                                pair, Convert.ToDecimal(ticker["Last"]),
                                Convert.ToDecimal(ticker["Bid"]),
                                Convert.ToDecimal(ticker["Ask"]),
                                Convert.ToDecimal(ticker["Volume"]),
                                Convert.ToDecimal(ticker["High"]),
                                Convert.ToDecimal(ticker["Low"]))
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