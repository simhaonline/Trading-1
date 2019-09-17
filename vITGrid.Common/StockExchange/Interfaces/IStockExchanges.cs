using System.Threading.Tasks;

namespace vITGrid.Common.StockExchange.Interfaces
{
    public interface IStockExchanges
    {
        Task<ITickers> GetTickers(string currencies, decimal exchangeRate);
    }
}