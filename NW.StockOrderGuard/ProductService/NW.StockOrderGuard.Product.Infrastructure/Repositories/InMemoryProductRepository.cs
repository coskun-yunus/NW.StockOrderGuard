using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Domain.Entities;

namespace NW.StockOrderGuard.Product.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<int, Domain.Entities.Product> _products = new();

        public Task<Domain.Entities.Product> GetByIdAsync(int id)
        {
            var product = _products.Values.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task SaveAsync(Domain.Entities.Product product)
        {
            _products.AddOrUpdate(
                product.ProductCode,
                product,
                (code, existing) => product
            );
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
        {
            return Task.FromResult(_products.Values.AsEnumerable());
        }
    }
} 