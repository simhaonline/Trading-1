using System.Collections.Generic;
using System.Threading.Tasks;
using vITGrid.Common.StockExchange.Interfaces;

namespace vITGrid.StockExchange
{
    public class TaskTickers
    {
        public async Task<List<ITickers>> GetTickers(List<string> stockExchanges, List<string> selectedPairs, decimal exchangeRate)
        {
            StockExchangeHelper stockExchangeHelper = new StockExchangeHelper();
            List<Task<ITickers>> taskTickers = new List<Task<ITickers>>();

            foreach(string stockExchange in stockExchanges)
            {
                switch(stockExchange)
                {
                    case "Bitfinex":
                        Bitfinex bitfinex = new Bitfinex();
                        string bitfinexPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.Bitfinex, "tBTCUSD,tLTCUSD,tLTCBTC,tETHUSD,tETHBTC,tETCUSD,tETCBTC,tZECUSD,tZECBTC,tXMRUSD,tXMRBTC,tDSHUSD,tDSHBTC,tXRPUSD,tXRPBTC,tIOTUSD,tIOTBTC,tIOTETH,tEOSUSD,tEOSBTC,tEOSETH,tSANUSD,tSANBTC,tSANETH,tOMGUSD,tOMGBTC,tOMGETH,tBCHUSD,tBCHBTC,tBCHETH,tNEOUSD,tNEOBTC,tNEOETH,tETPUSD,tETPBTC,tETPETH,tEDOUSD,tEDOBTC,tEDOETH,tBTGUSD,tBTGBTC,tRRTUSD,tRRTBTC,tQTMUSD,tQTMBTC,tQTMETH,tAVTUSD,tAVTBTC,tAVTETH,tDATUSD,tDATBTC,tDATETH,tQSHUSD,tQSHBTC,tQSHETH,tYYWUSD,tYYWBTC,tYYWETH,tGNTUSD,tGNTBTC,tGNTETH,tSNTUSD,tSNTBTC,tSNTETH,tBATUSD,tBATBTC,tBATETH,tMNAUSD,tMNABTC,tMNAETH,tFUNUSD,tFUNBTC,tFUNETH,tZRXUSD,tZRXBTC,tZRXETH,tTNBUSD,tTNBBTC,tTNBETH,tSPKUSD,tSPKBTC,tSPKETH,tTRXUSD,tTRXBTC,tTRXETH,tRCNUSD,tRCNBTC,tRCNETH,tRLCUSD,tRLCBTC,tRLCETH,tAIDUSD,tAIDBTC,tAIDETH,tSNGUSD,tSNGBTC,tSNGETH,tREPUSD,tREPBTC,tREPETH,tELFUSD,tELFBTC,tELFETH", selectedPairs);
                        taskTickers.Add(bitfinex.GetTickers(bitfinexPairs, exchangeRate));
                        break;
                    case "Binance":
                        Binance binance = new Binance();
                        Task<ITickers> taskBinanceTickers = binance.GetTickers(string.Empty, exchangeRate);
                        taskTickers.Add(taskBinanceTickers);
                        break;
                    case "Bitstamp":
                        Bitstamp bitstamp = new Bitstamp();
                        string bitstampPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.Bitstamp, "btcusd,xrpusd,xrpbtc,ltcusd,ltcbtc,ethusd,ethbtc,bchusd,bchbtc", selectedPairs);
                        Task<ITickers> taskBitstampTickers = bitstamp.GetTickers(bitstampPairs, exchangeRate);
                        taskTickers.Add(taskBitstampTickers);
                        break;
                    case "Bittrex":
                        Bittrex bittrex = new Bittrex();
                        Task<ITickers> taskBittrexTickers = bittrex.GetTickers(string.Empty, exchangeRate);
                        taskTickers.Add(taskBittrexTickers);
                        break;
                    case "Cex":
                        Cex cex = new Cex();
                        const string cexPairs = "BTC/USD/LTC/ETH/XRP/DASH/BCH/BTG/ZEC/GHS/XLM";
                        Task<ITickers> taskCexTickers = cex.GetTickers(cexPairs, exchangeRate);
                        taskTickers.Add(taskCexTickers);
                        break;
                    case "Exmo":
                        Exmo exmo = new Exmo();
                        Task<ITickers> taskExmoTickers = exmo.GetTickers(string.Empty, exchangeRate);
                        taskTickers.Add(taskExmoTickers);
                        break;
                    case "Gdax":
                        Gdax gdax = new Gdax();
                        string gdaxPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.Gdax, "BTC-USD,BTC-GBP,ETH-USD,ETH-BTC,LTC-USD,LTC-BTC,BCH-USD", selectedPairs);
                        Task<ITickers> taskGdaxTickers = gdax.GetTickers(gdaxPairs, exchangeRate);
                        taskTickers.Add(taskGdaxTickers);
                        break;
                    case "Gemini":
                        Gemini gemini = new Gemini();
                        string geminiPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.Gemini, "btcusd,ethbtc,ethusd", selectedPairs);
                        Task<ITickers> taskGeminiTickers = gemini.GetTickers(geminiPairs, exchangeRate);
                        taskTickers.Add(taskGeminiTickers);
                        break;
                    case "Poloniex":
                        Poloniex poloniex = new Poloniex();
                        Task<ITickers> taskPoloniexTickers = poloniex.GetTickers(string.Empty, exchangeRate);
                        taskTickers.Add(taskPoloniexTickers);
                        break;
                    case "QuadrigaCx":
                        QuadrigaCx quadrigaCx = new QuadrigaCx();
                        string quadrigaCxPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.QuadrigaCx, "btc_cad,btc_usd,eth_btc,eth_cad,ltc_cad,bch_cad,btg_cad", selectedPairs);
                        Task<ITickers> taskQuadrigaCxTickers = quadrigaCx.GetTickers(quadrigaCxPairs, exchangeRate);
                        taskTickers.Add(taskQuadrigaCxTickers);
                        break;
                    case "Bitmex":
                        Bitmex bitmex = new Bitmex();
                        Task<ITickers> taskBitmexTickers = bitmex.GetTickers(string.Empty, exchangeRate);
                        taskTickers.Add(taskBitmexTickers);
                        break;
                    case "Kraken":
                        Kraken kraken = new Kraken();
                        string krakenPairs = await stockExchangeHelper.GetSelectedPairs(Common.StockExchange.Types.StockExchange.Kraken, "BCHUSD,BCHXBT,DASHEUR,DASHUSD,DASHXBT,EOSETH,EOSXBT,GNOETH,GNOXBT,USDTUSD,ETCETH,ETCXBT,ETCUSD,ETHXBT,ETHUSD,ICNETH,ICNXBT,LTCXBT,LTCUSD,MLNETH,MLNXBT,REPETH,REPXBT,XBTUSD,XDGXBT,XLMXBT,XMRXBT,XMRUSD,XRPXBT,XRPUSD,ZECXBT,ZECUSD", selectedPairs);
                        Task<ITickers> taskKrakenTickers = kraken.GetTickers(krakenPairs, exchangeRate);
                        taskTickers.Add(taskKrakenTickers);
                        break;
                }
            }

            await Task.WhenAll(taskTickers);

            return await GetTickers(taskTickers);
        }

        private async Task<List<ITickers>> GetTickers(List<Task<ITickers>> taskTickers)
        {
            List<ITickers> tickers = new List<ITickers>();

            await Task.Run(() =>
            {
                foreach(Task<ITickers> taskTicker in taskTickers)
                {
                    tickers.Add(taskTicker.Result);
                }
            });
            
            return tickers;
        }
    }
}