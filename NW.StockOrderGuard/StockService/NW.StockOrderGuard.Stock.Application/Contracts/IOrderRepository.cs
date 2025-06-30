using System.Collections.Generic;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Application.Contracts
{
    public interface IOrderRepository
    {
        Task AddOrdersAsync(IEnumerable<Order> orders);
        Task<List<Order>> GetAllOrdersAsync();
    }
} 