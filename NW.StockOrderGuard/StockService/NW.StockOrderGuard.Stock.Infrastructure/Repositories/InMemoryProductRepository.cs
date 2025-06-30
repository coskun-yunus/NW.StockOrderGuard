using System.Collections.Concurrent;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<int, Product> _products = new();
        public Task SaveAsync(Product product)
        {
            _products[product.ProductCode] = product;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_products.Values);
        }

        public Task<IEnumerable<Product>> GetLowStockAsync()
        {
            var lowStock = _products.Values.Where(p => p.CurrentStock < p.ThresholdStock);
            return Task.FromResult<IEnumerable<Product>>(lowStock);
        }
    }
} 