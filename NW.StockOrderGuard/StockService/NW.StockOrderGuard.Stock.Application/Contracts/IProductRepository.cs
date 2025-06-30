using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Application.Contracts
{
    public interface IProductRepository
    {
        Task SaveAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetLowStockAsync();
    }
} 