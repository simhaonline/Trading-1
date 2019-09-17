using System.Net;

namespace vITGrid.Common.StockExchange.Exceptions
{
    public class StockExchangeException : WebException
    {
        public StockExchangeException(WebException exception, string message) : base(message, exception)
        {
            
        }
    }
}