namespace vITGrid.StockExchange.Secure
{
    public class BaseRequest
    {
        public BaseRequest(string request, string nonce)
        {
            Request = request;
            Nonce = nonce;
        }

        public string Request { get; set; }
        public string Nonce { get; set; }
    }
}