namespace NW.StockOrderGuard.Stock.API.Dto
{
    public class OrderDto
    {
        public int ProductCode { get; set; }
        public string Title { get; set; }
        public decimal BestPrice { get; set; }
        public int Quantity { get; set; }
    }
} 