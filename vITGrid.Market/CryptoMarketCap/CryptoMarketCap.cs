using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.Market.CryptoMarketCap
{
    public class CryptoMarketCap
    {
        private const string BaseUri = "https://api.coinmarketcap.com/v1";
        private HttpWebRequest _httpWebRequest;
        private HttpWebResponse _httpWebResponse;
        private StreamReader _streamReader;

        public CryptoMarketCap()
        {

        }

        public async Task<GenericTickers> GetTickers(int limit)
        {
            GenericTickers tickers = new GenericTickers();

            try
            {
                string uri = $"{BaseUri}/ticker/?limit={limit}";

                _httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;

                if(_httpWebRequest != null)
                {
                    _httpWebRequest.Method = "GET";
                    _httpWebRequest.Accept = "application/json";

                    using(_httpWebResponse = await _httpWebRequest.GetResponseAsync() as HttpWebResponse)
                    {
                        if(_httpWebResponse != null)
                        {
                            _streamReader = new StreamReader(_httpWebResponse.GetResponseStream() ?? throw new InvalidOperationException());
                        }

                        JArray resultsJson = JsonConvert.DeserializeObject<JArray>(_streamReader.ReadToEnd());
                        foreach(JObject ticker in resultsJson.Children<JObject>())
                        {
                            tickers.Tickers.Add(new GenericTicker(
                                ticker["id"].ToString(), 
                                ticker["name"].ToString(), 
                                ticker["symbol"].ToString(), 
                                ticker["rank"].ToString(), 
                                ticker["price_usd"].ToString(), 
                                ticker["price_btc"].ToString(), 
                                ticker["24h_volume_usd"].ToString(), 
                                ticker["market_cap_usd"].ToString(), 
                                ticker["available_supply"].ToString(), 
                                ticker["total_supply"].ToString(), 
                                ticker["max_supply"].ToString(), 
                                ticker["percent_change_1h"].ToString(), 
                                ticker["percent_change_24h"].ToString(), 
                                ticker["percent_change_7d"].ToString(), 
                                ticker["last_updated"].ToString())
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