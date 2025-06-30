using NW.StockOrderGuard.SharedKernel;
using NW.StockOrderGuard.Product.Domain.ValueObjects;


namespace NW.StockOrderGuard.Product.Domain.Entities
{
  
    public class Product : IAggregateRoot 
    {
        public int Id { get; private set; }
        public int ProductCode { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public Image Image { get; private set; }
        public Rating Rating { get; private set; } 
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public Category Category { get; private set; } 
        public int ThresholdStock { get; private set; }
        public int CurrentStock { get; private set; }
        public ICollection<ProductOffer> Offers { get; private set; } = new List<ProductOffer>();

        protected Product() { Offers = new List<ProductOffer>(); }

        public Product(int productCode, string title, decimal price, string description, Image image, Rating rating, int stockQuantity, bool isActive, Category category, int thresholdStock, int currentStock)
        {
            ProductCode = productCode;
            Title = title;
            Price = price;
            Description = description;
            Image = image;
            Rating = rating;
            StockQuantity = stockQuantity;
            IsActive = isActive;
            Category = category;
            ThresholdStock = thresholdStock;
            CurrentStock = currentStock;
            Offers = new List<ProductOffer>();
        }


        // Domain metotları
        public void UpdateDetails(string title, decimal price, string description, Image image, Category category)
        {
            Title = title;
            Price = price;
            Description = description;
            Image = image;
            Category = category;
        }

        public void UpdateRating(Rating rating)
        {
            Rating = rating;
        }

        public void IncreaseStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Tutar pozitif olmalıdır.");
            StockQuantity += amount;
        }

        public void DecreaseStock(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            if (StockQuantity - amount < 0) throw new InvalidOperationException("Stok yetersiz.");
            StockQuantity -= amount;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateStock(int thresholdStock, int currentStock)
        {
            ThresholdStock = thresholdStock;
            CurrentStock = currentStock;
        }

        public void AddOffer(ProductOffer offer)
        {
            Offers.Add(offer);
        }
        public decimal GetBestOffer()
        {
            if (Offers.Count > 0)
                return Offers.Min(o => o.Price);
            else
                return Price;
        }
    }
}