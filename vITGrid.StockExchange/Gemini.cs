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
    public class Gemini : StockExchangeBase, IStockExchanges
    {
        public Gemini()
        {
            BaseUri = "https://api.gemini.com/v1";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Gemini);

            try
            {
                foreach(string currency in currencies.Split(','))
                {
                    string uri = $"{BaseUri}/pubTicker/{currency}";

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

                            tickers.PairTickers.Add(new PairTicker(
                                currency.Insert(3, "/").ToUpper(),
                                Convert.ToDecimal(resultsJson["last"]),
                                Convert.ToDecimal(resultsJson["bid"]),
                                Convert.ToDecimal(resultsJson["ask"]))
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