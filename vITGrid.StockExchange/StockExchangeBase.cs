using System.IO;
using System.Net;

namespace vITGrid.StockExchange
{
    public abstract class StockExchangeBase
    {
        protected string BaseUri;
        protected HttpWebRequest HttpWebRequest;
        protected HttpWebResponse HttpWebResponse;
        protected StreamReader StreamReader;
    }
}