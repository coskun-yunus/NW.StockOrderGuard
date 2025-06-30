using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NW.StockOrderGuard.Stock.Application.Dto;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Domain.Entities;

namespace NW.StockOrderGuard.Stock.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var result = orders.Select(o => new OrderDto
            {
                ProductCode = o.ProductCode,
                Title = o.Title,
                BestPrice = o.BestPrice,
                Quantity = o.Quantity
            }).ToList();
            return result;
        }
    }
} 