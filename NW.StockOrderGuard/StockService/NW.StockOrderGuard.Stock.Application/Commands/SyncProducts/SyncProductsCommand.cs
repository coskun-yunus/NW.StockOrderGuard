using MediatR;
using NW.StockOrderGuard.Stock.Application.IntegrationEvents;

namespace NW.StockOrderGuard.Stock.Application.Commands.SyncProducts
{
    public class SyncProductsCommand : IRequest<Unit>
    {
        public ProductSyncedEvent Event { get; }
        public SyncProductsCommand(ProductSyncedEvent @event)
        {
            Event = @event;
        }
    }
} 