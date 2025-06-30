namespace NW.StockOrderGuard.Product.Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int ProductCode { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal RatingRate { get; set; }
        public int RatingCount { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool CategoryIsActive { get; set; }
        public int ThresholdStock { get; set; }
        public int CurrentStock { get; set; }
        public decimal BestPrice { get; set; }
    }
} 