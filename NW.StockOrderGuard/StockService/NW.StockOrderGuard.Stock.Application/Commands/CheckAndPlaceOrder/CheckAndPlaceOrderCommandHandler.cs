using MediatR;
using NW.StockOrderGuard.Stock.Application.Dto;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Domain.Entities;
using NW.StockOrderGuard.Stock.Application.Queries.GetLowStockProducts;

namespace NW.StockOrderGuard.Stock.Application.Commands.CheckAndPlaceOrder
{
    public class CheckAndPlaceOrderCommandHandler : IRequestHandler<CheckAndPlaceOrderCommand, List<OrderDto>>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public CheckAndPlaceOrderCommandHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(CheckAndPlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var lowStockProducts = await _mediator.Send(new GetLowStockProductsQuery(), cancellationToken);
            var newOrders = new List<Order>();
            foreach (var product in lowStockProducts)
            {
                var order = new Order
                (
                     product.ProductCode,
                     product.Title,
                     product.BestPrice,
                     1
                );
                newOrders.Add(order);
            }
            await _orderRepository.AddOrdersAsync(newOrders);
            var result = newOrders.Select(o => new OrderDto
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