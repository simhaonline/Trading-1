namespace vITGrid.StockExchange.Secure
{
    public class NewOrderRequest : BaseRequest
    {
        public NewOrderRequest(string request, string nonce) : base(request, nonce)
        {

        }

        public string Pair { get; set; }
        public string Amount { get; set; }
        public string Price { get; set; }
        public string Exchange { get; set; }
        public string Side { get; set; }
        public string OrderType { get; set; }
    }
}