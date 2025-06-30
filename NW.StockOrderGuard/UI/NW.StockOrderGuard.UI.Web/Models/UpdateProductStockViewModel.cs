namespace NW.StockOrderGuard.UI.Web.Models
{
    public class UpdateProductStockViewModel
    {
        public string ProductName { get; set; }
        public int ThresholdStock { get; set; }
        public int CurrentStock { get; set; }
        public string ResultMessage { get; set; }
    }
} 