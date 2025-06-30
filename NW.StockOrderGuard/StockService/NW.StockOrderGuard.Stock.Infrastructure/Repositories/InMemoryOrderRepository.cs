using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Application.Dto;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static readonly List<Order> _orders = new List<Order>();

        public Task AddOrdersAsync(IEnumerable<Order> orders)
        {
            _orders.AddRange(orders);
            return Task.CompletedTask;
        }

        public Task<List<Order>> GetAllOrdersAsync()
        {
            return Task.FromResult(_orders.ToList());
        }
    }
} 