using NW.StockOrderGuard.SharedKernel;
using System;

namespace NW.StockOrderGuard.Stock.Domain.Entities
{
    public class Order : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public int ProductCode { get; private set; }
        public string Title { get; private set; }
        public decimal BestPrice { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Order() { }

        public Order(int productCode, string title, decimal bestPrice, int quantity)
        {
            Id = Guid.NewGuid();
            ProductCode = productCode;
            Title = title;
            BestPrice = bestPrice;
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Miktar pozitif olmalıdır.");
            Quantity = quantity;
        }
    }
} 