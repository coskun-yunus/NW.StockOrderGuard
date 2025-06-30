namespace NW.StockOrderGuard.Stock.Application.IntegrationEvents
{
    public class ProductSyncedEvent
    {
        public List<ProductSyncedDto> Products { get; set; } = new();
    }

    public class ProductSyncedDto
    {
        public int ProductCode { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal BestPrice { get; set; }
        public int CurrentStock { get; set; }
        public int ThresholdStock { get; set; }
    }
} 