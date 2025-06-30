using Microsoft.AspNetCore.Mvc;
using MediatR;
using NW.StockOrderGuard.Product.Application.Queries;
using NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock;
using NW.StockOrderGuard.Product.Application.Commands.SyncProducts;

namespace NW.StockOrderGuard.Product.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("products/sync")]
        public async Task<IActionResult> SyncProducts()
        {
            var result = await _mediator.Send(new SyncProductsCommand());
            return Ok(new { message = "Ürün kataloğu başarıyla senkronize edildi." });
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpPost("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductStockCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });
            return Ok(new { message = "Ürün stoğu başarıyla güncellendi." });
        }

        [HttpGet("products/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _mediator.Send(new GetProductByNameQuery(name));
            if (product == null)
                return NotFound(new { message = "Product not found." });
            return Ok(product);
        }
    }
} 