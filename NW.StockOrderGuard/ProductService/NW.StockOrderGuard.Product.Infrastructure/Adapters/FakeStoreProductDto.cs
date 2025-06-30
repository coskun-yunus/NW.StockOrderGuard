namespace NW.StockOrderGuard.Product.Infrastructure.Adapters
{
    public class FakeStoreProductDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public RatingDto? rating { get; set; }
    }

    public class RatingDto
    {
        public decimal? rate { get; set; }
        public int? count { get; set; }
    }
} 