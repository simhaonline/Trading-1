using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vITGrid.Common.Api.Exceptions;
using vITGrid.Common.Common.Models;
using vITGrid.Common.StockExchange.Interfaces;
using vITGrid.StockExchange;

namespace vITGrid.Api.Controllers.StockExchange
{
    [Route("api/[controller]")]
    public class StockExchangeController : Controller
    {
        [HttpPost("GetTickers")]
        public async Task<List<ITickers>> GetTickers([FromBody] CompositeStockExchangeObjects compositeStockExchangeObjects)
        {
            decimal exchangeRate = compositeStockExchangeObjects.ExchangeRate;
            List<string> stockExchanges = compositeStockExchangeObjects.StockExchanges;
            List<string> selectedPairs = compositeStockExchangeObjects.SelectedPairs;

            TaskTickers taskTickers = new TaskTickers();
            List<ITickers> results = await taskTickers.GetTickers(stockExchanges, selectedPairs, exchangeRate);

            return results;
        }

        [HttpPost("GetTicker")]
        public async Task<ITickers> GetTicker([FromBody] CompositeStockExchangeObject compositeStockExchangeObject)
        {
            decimal exchangeRate = compositeStockExchangeObject.ExchangeRate;
            Common.StockExchange.Types.StockExchange selectedStockExchange = compositeStockExchangeObject.StockExchange;
            string selectedPairs = compositeStockExchangeObject.SelectedPairs;

            IStockExchanges stockExchanges;

            switch(selectedStockExchange)
            {
                case Common.StockExchange.Types.StockExchange.Binance:
                    stockExchanges = new Binance();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Bitfinex:
                    stockExchanges = new Bitfinex();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Bitmex:
                    stockExchanges = new Bitmex();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Bitstamp:
                    stockExchanges = new Bitstamp();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Bittrex:
                    stockExchanges = new Bittrex();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Cex:
                    stockExchanges = new Cex();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Exmo:
                    stockExchanges = new Exmo();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Gdax:
                    stockExchanges = new Gdax();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Gemini:
                    stockExchanges = new Gemini();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Kraken:
                    stockExchanges = new Kraken();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.Poloniex:
                    stockExchanges = new Poloniex();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                case Common.StockExchange.Types.StockExchange.QuadrigaCx:
                    stockExchanges = new QuadrigaCx();
                    return await stockExchanges.GetTickers(selectedPairs, exchangeRate);
                default:
                    throw new NullStockExchangeException("Invalid StockExchange Selection");
            }
        }
    }
}