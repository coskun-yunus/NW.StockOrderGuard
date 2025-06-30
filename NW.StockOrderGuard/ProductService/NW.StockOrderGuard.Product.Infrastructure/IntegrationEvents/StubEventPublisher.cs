using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NW.StockOrderGuard.Product.Application.IntegrationEvents;

namespace NW.StockOrderGuard.Product.Infrastructure.IntegrationEvents
{
    public class StubEventPublisher : IEventPublisher
    {
        private readonly ILogger<StubEventPublisher> _logger;
        public StubEventPublisher(ILogger<StubEventPublisher> logger)
        {
            _logger = logger;
        }

        // Event publish
        // Not: RabbitMQ gibi bir mesajlaşma altyapısı kurulmadığından, bu noktada event publish işlemi yerine HTTP POST ile StockService'e veri gönderilebilir.
        // Mevcut implementasyon ise sadece log'a yazar ve başka bir servise veri göndermez.
        public Task PublishProductSyncedAsync(ProductSyncedEvent @event)
        {
            _logger.LogInformation("ProductSyncedEvent published with {Count} products.", @event.Products.Count);
            return Task.CompletedTask;
        }
    }
} 