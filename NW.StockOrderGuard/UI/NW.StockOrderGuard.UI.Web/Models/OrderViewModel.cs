namespace NW.StockOrderGuard.UI.Web.Models
{
    public class OrderViewModel
    {
        public int ProductCode { get; set; }
        public string Title { get; set; }
        public decimal BestPrice { get; set; }
        public int Quantity { get; set; }
    }
}
