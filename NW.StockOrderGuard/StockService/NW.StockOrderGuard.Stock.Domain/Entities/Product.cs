using NW.StockOrderGuard.SharedKernel;
using System;

namespace NW.StockOrderGuard.Stock.Domain.Entities
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public int ProductCode { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public decimal BestPrice { get; private set; }
        public int ThresholdStock { get; private set; }
        public int CurrentStock { get; private set; }

        protected Product() { }

        public Product(int productCode, string title, decimal price, decimal bestPrice, int currentStock = 0, int thresholdStock = 10)
        {
            Id = Guid.NewGuid();
            ProductCode = productCode;
            Title = title;
            Price = price;
            BestPrice = bestPrice;
            CurrentStock = currentStock;
            ThresholdStock = thresholdStock;
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new ArgumentException("Stok negatif olamaz.");
            CurrentStock = newStock;
        }

        public void UpdateThreshold(int newThreshold)
        {
            if (newThreshold < 0)
                throw new ArgumentException("Eşik negatif olamaz.");
            ThresholdStock = newThreshold;
        }
    }
} 