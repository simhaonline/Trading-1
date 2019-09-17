using System;

namespace vITGrid.Common.Api.Exceptions
{
    public class NullStockExchangeException : Exception
    {
        public NullStockExchangeException(string message, Exception exception) : base(message, exception)
        {
            
        }

        public NullStockExchangeException(string message) : base(message)
        {

        }
    }
}