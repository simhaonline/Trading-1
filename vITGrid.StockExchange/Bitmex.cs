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
    public class Bitmex : StockExchangeBase, IStockExchanges
    {
        public Bitmex()
        {
            BaseUri = "https://www.bitmex.com/api/v1";
        }

        public async Task<ITickers> GetTickers(string currencies, decimal exchangeRate)
        {
            string uri = $"{BaseUri}/instrument/active{currencies}";

            ITickers tickers = new Tickers(Common.StockExchange.Types.StockExchange.Bitmex);

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
                            string baseCurrency = ticker["underlying"].ToString();
                            string quoteCurrency = ticker["quoteCurrency"].ToString();

                            string pair = $"{baseCurrency}/{quoteCurrency}";
                            decimal lastPrice = string.IsNullOrEmpty(ticker["lastPrice"].ToString()) ? 0m : Convert.ToDecimal(ticker["lastPrice"]);
                            decimal bidPrice = string.IsNullOrEmpty(ticker["bidPrice"].ToString()) ? 0m : Convert.ToDecimal(ticker["bidPrice"]);
                            decimal askPrice = string.IsNullOrEmpty(ticker["askPrice"].ToString()) ? 0m : Convert.ToDecimal(ticker["askPrice"]);
                            decimal volume = string.IsNullOrEmpty(ticker["volume"].ToString()) ? 0m : Convert.ToDecimal(ticker["volume"]);
                            decimal highPrice = string.IsNullOrEmpty(ticker["highPrice"].ToString()) ? 0m : Convert.ToDecimal(ticker["highPrice"]);
                            decimal lowPrice = string.IsNullOrEmpty(ticker["lowPrice"].ToString()) ? 0m : Convert.ToDecimal(ticker["lowPrice"]);

                            pair = pair.Replace("XBT", "BTC").Replace("DASH", "DSH");

                            if(!IsPairExist(tickers.PairTickers, pair))
                            {
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

        private bool IsPairExist(List<IPairTicker> pairTickers, string pair)
        {
            foreach(IPairTicker pairTicker in pairTickers)
            {
                if(pairTicker.Pair.Equals(pair))
                {
                    return true;
                }
            }

            return false;
        }
    }
}