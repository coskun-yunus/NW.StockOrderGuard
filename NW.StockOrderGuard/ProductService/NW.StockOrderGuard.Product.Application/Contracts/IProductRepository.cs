using System.Collections.Generic;
using System.Threading.Tasks;
using NW.StockOrderGuard.Product.Domain.Entities;

namespace NW.StockOrderGuard.Product.Application.Contracts
{
    public interface IProductRepository
    {
        Task<Domain.Entities.Product> GetByIdAsync(int id);
        Task SaveAsync(Domain.Entities.Product product);
        Task<IEnumerable<Domain.Entities.Product>> GetAllAsync();
    }
} 