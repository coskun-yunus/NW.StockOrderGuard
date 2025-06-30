using Microsoft.AspNetCore.Mvc;
using MediatR;
using NW.StockOrderGuard.Stock.Application.Commands.CheckAndPlaceOrder;
using NW.StockOrderGuard.Stock.Application.Contracts;
using NW.StockOrderGuard.Stock.Application.Queries.GetAllOrders;

namespace NW.StockOrderGuard.Stock.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        [HttpPost("orders/check-and-place")]
        public async Task<IActionResult> CheckAndPlaceOrder()
        {
            var result = await _mediator.Send(new CheckAndPlaceOrderCommand());
            return Ok(result);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }
    }
} 