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
    public class Bitfinex : StockExchangeBase, IStockExchanges
    {
        public Bitfinex()
        {
            BaseUri = "https://api.bitfinex.com/v2";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            string uri = $"{BaseUri}/tickers?symbols={currencies}";

            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Bitfinex);

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

                        JArray resultsJson = JsonConvert.DeserializeObject<JArray>(StreamReader.ReadToEnd());

                        foreach(JToken ticker in resultsJson)
                        {
                            string pair = ticker[0].ToString().Remove(0, 1).Insert(3, "/");
                            tickers.PairTickers.Add(new PairTickerVolumeHighLowPrice(
                                pair, 
                                Convert.ToDecimal(ticker[7]),
                                Convert.ToDecimal(ticker[1]),
                                Convert.ToDecimal(ticker[3]),
                                Convert.ToDecimal(ticker[8]),
                                Convert.ToDecimal(ticker[9]),
                                Convert.ToDecimal(ticker[10]))
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