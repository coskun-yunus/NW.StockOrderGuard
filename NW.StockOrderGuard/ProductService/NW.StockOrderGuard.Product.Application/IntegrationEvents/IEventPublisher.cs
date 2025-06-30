using System.Threading.Tasks;

namespace NW.StockOrderGuard.Product.Application.IntegrationEvents
{
    public interface IEventPublisher
    {
        Task PublishProductSyncedAsync(ProductSyncedEvent @event);
    }
} 