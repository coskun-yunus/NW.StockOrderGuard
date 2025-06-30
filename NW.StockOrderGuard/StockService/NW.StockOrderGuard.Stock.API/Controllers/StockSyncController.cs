using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using NW.StockOrderGuard.Stock.Application.IntegrationEvents;
using NW.StockOrderGuard.Stock.Application.Commands.SyncProducts;

namespace NW.StockOrderGuard.Stock.API.Controllers
{
    [ApiController]
    [Route("api/stock/sync")]
    public class StockSyncController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockSyncController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SyncProducts([FromBody] ProductSyncedEvent @event)
        {
            await _mediator.Send(new SyncProductsCommand(@event));
            return Ok();
        }
    }
} 