using Microsoft.AspNetCore.Mvc;
using NW.StockOrderGuard.Stock.Domain.Entities;
using MediatR;
using NW.StockOrderGuard.Stock.Application.Queries;
using NW.StockOrderGuard.Stock.Application.Queries.GetLowStockProducts;

namespace NW.StockOrderGuard.Stock.API.Controllers
{
    [ApiController]
    [Route("api/stock/")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLowStock()
        {
            var products = await _mediator.Send(new GetLowStockProductsQuery());
            return Ok(products);
        }
    }
} 