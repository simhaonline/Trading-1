using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using vITGrid.Common.StockExchange.Exceptions;
using vITGrid.Common.StockExchange.Models;

namespace vITGrid.StockExchange.Secure
{
    public class BitfinexSecure
    {
        private const string ApiKey = "your api key";
        private const string ApiSecret = "your api secret";
        private const string BaseUri = "https://api.bitfinex.com/";
        private readonly DateTime _dateTime = new DateTime(1970, 1, 1);
        private int _nonce = 0;
        private readonly HMACSHA384 _hmacsha384;

        public BitfinexSecure()
        {
            _hmacsha384 = new HMACSHA384(Encoding.UTF8.GetBytes(ApiSecret));
        }

        public AccountBalance GetAccountBalance(Common.StockExchange.Types.StockExchange stockExchange, string currency)
        {
            AccountBalance accountBalance = new AccountBalance(stockExchange.ToString());

            BaseRequest baseRequest = new BaseRequest("/v1/balances", GetNonce());
            string response = SendRequest(baseRequest, "POST");

            JArray resultsJson = JsonConvert.DeserializeObject<JArray>(response);
            foreach(JToken ticker in resultsJson)
            {
                string account = ticker["type"].ToString();
                string currencyTicker = ticker["currency"].ToString().ToUpper();
                decimal availableBalance = Convert.ToDecimal(ticker["available"]);
                decimal amountBalance = Convert.ToDecimal(ticker["amount"]);

                if(currencyTicker.Equals(currency))
                {
                    accountBalance.Balances.Add(new Balance(account, currencyTicker, availableBalance, amountBalance));
                }
            }

            return accountBalance;
        }

        public void NewOrder(Common.StockExchange.Types.StockExchange stockExchange, string currency)
        {
            //AccountBalance accountBalance = new AccountBalance(stockExchange.ToString());

            BaseRequest baseRequest = new BaseRequest("/v1/order/new", GetNonce());
            string response = SendRequest(baseRequest, "POST");

            JArray resultsJson = JsonConvert.DeserializeObject<JArray>(response);
            foreach(JToken ticker in resultsJson)
            {
                //string account = ticker["type"].ToString();
                //string currencyTicker = ticker["currency"].ToString().ToUpper();
                //decimal availableBalance = Convert.ToDecimal(ticker["available"]);
                //decimal amountBalance = Convert.ToDecimal(ticker["amount"]);

                //if(currencyTicker.Equals(currency))
                //{
                //    //accountBalance.Balances.Add(new Balance(account, currencyTicker, availableBalance, amountBalance));
                //}
            }
        }

        private string GetNonce()
        {
            if(_nonce == 0) 
            {
                _nonce = (int)(DateTime.UtcNow - _dateTime).TotalSeconds;
            }
            return (_nonce++).ToString();
        }
        
        private string GetHexString(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
            foreach(byte b in bytes)
            {
                stringBuilder.Append($"{b:x2}");
            }
            return stringBuilder.ToString();
        }

        private string SendRequest(BaseRequest request, string httpMethod)
        {
            string json = JsonConvert.SerializeObject(request).ToLower();
            string json64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            byte[] data = Encoding.UTF8.GetBytes(json64);
            byte[] hash = _hmacsha384.ComputeHash(data);
            string signature = GetHexString(hash);

            HttpWebRequest httpWebRequest = WebRequest.Create(string.Concat(BaseUri, request.Request)) as HttpWebRequest;
            httpWebRequest.Headers.Add("X-BFX-APIKEY", ApiKey);
            httpWebRequest.Headers.Add("X-BFX-PAYLOAD", json64);
            httpWebRequest.Headers.Add("X-BFX-SIGNATURE", signature);
            httpWebRequest.Method = httpMethod;

            string response = null;
            try
            {
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                response = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch(WebException exception)
            {
                StreamReader streamReader = new StreamReader(exception.Response.GetResponseStream());
                response = streamReader.ReadToEnd();
                streamReader.Close();
                throw new StockExchangeException(exception, response);
            }
            return response;
        }
    }
}