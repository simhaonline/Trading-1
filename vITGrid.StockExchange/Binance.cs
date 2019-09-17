using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.StockExchange
{
    public class Binance : StockExchangeBase, IStockExchanges
    {
        public Binance()
        {
            BaseUri = "https://api.binance.com/api/v3";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Binance);

            try
            {
                foreach(string currency in currencies.Split(','))
                {
                    string uri = $"{BaseUri}/ticker/bookTicker{currency}";

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
                                tickers.PairTickers.Add(new PairTicker(
                                    ticker["symbol"].ToString().Insert(3, "/"),
                                    Convert.ToDecimal(ticker["askPrice"]),
                                    Convert.ToDecimal(ticker["bidPrice"]),
                                    Convert.ToDecimal(ticker["askPrice"]))
                                );
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