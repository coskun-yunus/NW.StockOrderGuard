using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Domain.ValueObjects
{
    public class ProductOffer : IValueObject
    {
        public string ProductCode { get; }
        public decimal Price { get; }

        public ProductOffer(string productCode, decimal price)
        {
            ProductCode = productCode;
            Price = price;
        }

        public override bool Equals(object obj) =>
            obj is ProductOffer other &&
            ProductCode == other.ProductCode &&
            Price == other.Price;

        public override int GetHashCode() =>
            HashCode.Combine(ProductCode, Price);
    }
} 